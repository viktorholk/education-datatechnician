<template>
  <div id="design-editor">

    <div class="canvas">
      <div @click="removeOverlay()" id="overlay">
        <p>Click here to start designing</p>
      </div>

      <canvas id="canvas" height="460" width="460"></canvas>
      <p class="description">Design outside of shirt will be ignored.</p>    
    </div>

    <div class="tools">
      <button @click="toggleShirtSelection()">PAGE</button>
      <p class="description">Design page</p>
      <button @click="clear()">CLEAR</button>
      <p class="description">Clear the design</p>
      <button @click="redo()">REDO</button>
      <p class="description">Redo last action</p>
      <input v-model="this.brush.size" type="range" min="1" max="10">
      <!-- <p class="description"> {{this.brush.size}} </p> -->
      <p class="description">Brush size</p>
      <input v-model="this.brush.color" type="color"  list="presets">
      <datalist id="presets">
        <option value="#FF0000">Red</option>
        <option value="#FFFF00">Yellow</option>
        <option value="#00FF00">Green</option>
        <option value="#00FFFF">Blue</option>
        <option value="#FF00FF">Pink</option>
        <option value="#9D00FF">Purple</option>
      </datalist>
      <p class="description">Brush color</p>
      <!-- <input v-model="this.data.text" type="text"> -->
      <button @click="saveImage()">Save</button>
      <p class="description">Save the design</p>
      
    </div>
  </div>
</template>

<script>
export default {
  name: 'DesignEditor',
  data(){
    return {
      canvas: null,
      context: null,
      states: {
        mouseDown: false
      },
      brush: {
        size: 3,
        color: '#FF0000'
      },
      design: {
        shirt: {
          images: ['/shirts/shirt-front.png', '/shirts/shirt-back.png'],
          selection: 0
        },
        colors: [

        ],
        // 0 Front shirt, 1 back shirt
        data: {
          lines: [[], []],
          images: [[], []],
          texts: [[], []]
        }
      },
      data: {
        text: ''
      }
    }
  },
  mounted(){
    this.canvas = document.getElementById('canvas')
    this.context = this.canvas.getContext('2d')

    this.draw()

    // Setup events
    this.canvas.addEventListener('mousedown', this.mousedown)
    this.canvas.addEventListener('mousemove', this.mousemove)
    this.canvas.addEventListener('mouseup', this.mouseup)
  },
  methods:{
    saveImage(){


        var link = document.createElement('a')

        link.setAttribute('download', 'download.png');
        link.setAttribute('href', this.canvas.toDataURL("image/png").replace("image/png", "image/octet-stream"));
        link.click();
    },
    removeOverlay(){
      document.getElementById("overlay").remove()
    },
    draw(){
      // clear canvas
      this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);

      // Draw the shirt 
      const image = new Image()
      image.height = 400
      image.width = 400
      image.src = this.design.shirt.images[this.design.shirt.selection]

      // Get the position of the image
      const x = (this.canvas.width - image.width) / 2
      const y = (this.canvas.height - image.height) / 2

      image.onload = () => {
        this.context.drawImage(image, x, y, image.height, image.width)
        // When the shirt has loaded apply the brush lines data
        const lineArray = this.design.data.lines[this.design.shirt.selection]

        for (var i = 1; i < lineArray.length; i++) {
          this.context.beginPath();
          this.context.moveTo(lineArray[i-1].x, lineArray[i-1].y);
          this.context.lineWidth  = lineArray[i].size;
          this.context.lineCap = "round";
          this.context.strokeStyle = lineArray[i].color;
          this.context.lineTo(lineArray[i].x, lineArray[i].y);
          this.context.stroke();
        }
      }
    },
    toggleShirtSelection(){
      if (this.design.shirt.selection == 0)
        this.design.shirt.selection = 1
      else this.design.shirt.selection = 0
      
      this.draw()
    },
    clear(){
      this.design.data.lines   = [[], []]
      this.design.data.images  = [[], []]
      this.design.data.texts   = [[], []]
      this.draw()
    },
    redo(){
      if (this.design.data.lines[this.design.shirt.selection].length > 0) {
        // 25% of design
        const startIndex = (this.design.data.lines[this.design.shirt.selection].length * 0.25)

        this.design.data.lines[this.design.shirt.selection].splice(this.design.data.lines[this.design.shirt.selection].length - startIndex, this.design.data.lines[this.design.shirt.selection].length)
        this.draw()
      }
    },
    mousedown(event) {
      const pos = this.getMousePos(event)

      this.states.mouseDown = true

      this.context.moveTo(pos.x, pos.y)
      this.context.beginPath()
      this.context.lineWidth = this.brush.size
      this.context.lineCap = 'round'
      this.context.strokeStyle = this.brush.color

    },
    mousemove(event) {
      if (this.states.mouseDown) {
        const pos = this.getMousePos(event)

        this.context.lineTo(pos.x, pos.y)
        this.context.stroke()

        this.store(pos.x, pos.y, this.brush.size, this.brush.color)
      }      
    },
    mouseup() {
      this.states.mouseDown = false
      this.store()
    },
		getMousePos(event) {
			var rect = this.canvas.getBoundingClientRect();
			return {
				x: event.clientX - rect.left,
				y: event.clientY - rect.top
			};
		},
    store(x, y, size, color){
      const data = {
        x: x,
        y: y,
        size: size,
        color: color
      }
      // Push the data to the correct array
      this.design.data.lines[this.design.shirt.selection].push(data)
    }
  }
}
</script>

<style scoped>

#design-editor {
  display: flex;
  align-items: center;
}
#design-editor .tools {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 460px;
  padding: 1em;
}
#design-editor .tools p {
  margin: 0;
  margin-bottom: 0.5em;
  font-size: 0.6em;
}

#design-editor canvas{
  border: 1px solid black;
  border-bottom: none;
}

#design-editor .canvas {
  position: relative;
}

#design-editor .canvas #overlay{
  background: rgba(255, 255, 255, 0.021);
  position: absolute;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  z-index: 10;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 1em;
}
#design-editor .canvas #overlay p {
  color: black !important;
  opacity: 0.5;
  font-size: 0.8em;
}


#design-editor .canvas .description {
  font-size: 0.8em;
  text-align: center;
  color: grey;
  opacity: 0.8;
  margin: 0;
}

</style>