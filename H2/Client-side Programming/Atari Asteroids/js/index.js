var canvas = document.getElementById('gameCanvas');
canvas.focus();
var ctx = canvas.getContext('2d');

var center = {
    x: (canvas.width / 2),
    y: (canvas.height / 2),
}

var player = {
    size: 25,
    movementSpeed: 5,
    input: {
        left: false,
        right: false,
        up: false,
        down: false
    },
    position: {
        x: center.x,
        y: center.y,
        angle: 0,
    },
    draw: function(){
        // console.log(this.movement.x,this.movement.y,this.movement.angle);

        ctx.save();

        ctx.translate(this.position.x, this.position.y)
        ctx.rotate(this.position.angle)

        ctx.beginPath();
        ctx.strokeStyle = "white";
        ctx.lineWidth = 2;

        ctx.lineTo(0,0)
        ctx.lineTo(-this.size / 2, this.size /2)
        ctx.lineTo(0,-this.size)     
        ctx.lineTo(this.size / 2, this.size / 2)   

        ctx.closePath();
        ctx.stroke();

        ctx.restore();
    },

    move: function(){
        if (player.input.left) {
            player.position.angle -= 0.05;
        }
        if (player.input.right) {
            player.position.angle += 0.05;
        }
        if (player.input.up){
            var xVector = Math.sin(this.position.angle);
            var yVector = Math.cos(this.position.angle);

            var magnitude = Math.sqrt(xVector*xVector + yVector*yVector);

            this.position.x = this.position.x + (xVector / magnitude) * player.movementSpeed;
            this.position.y = this.position.y - (yVector / magnitude) * player.movementSpeed;
        }
    }
}

function update() {

    if (player.position.angle > 360 || player.position.angle < -360 )
        player.position.angle = 0;

    // Border detection
    // X
    if (player.position.x > canvas.width) {
        player.position.x = 0;
    }
    if (player.position.x < 0) {
        player.position.x = canvas.width;
    }
    // Y
    if (player.position.y > canvas.height){
        player.position.y = 0;
    }
    if (player.position.y < 0) {
        player.position.y = canvas.height;
    }

}

function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    player.draw();
}

function loop() {
    update();
    draw();

    player.move();
    window.requestAnimationFrame(loop);
}
// Listeners
canvas.addEventListener('keydown', (event) => {
    const key = event.key;
    // Left input
    if (key == "a" || key == "ArrowLeft") {
        player.input.left = true;
    }
    // Right input
    if (key == "d" || key == "ArrowRight") {
        player.input.right = true;
    }
    // Up input
    if (key == "w" || key == "ArrowUp") {
        player.input.up = true;
    }
    // Down input
    if (key == "s" || key == "ArrowDown") {
        player.input.down = true;
    }
})
canvas.addEventListener('keyup', event => {
    const key = event.key;
    // Left input
    if (key == "a" || key == "ArrowLeft") {
        player.input.left = false;
    }
    // Right input
    if (key == "d" || key == "ArrowRight") {
        player.input.right = false;
    }
    // Up input
    if (key == "w" || key == "ArrowUp") {
        player.input.up = false;
    }
    // Down input
    if (key == "s" || key == "ArrowDown") {
        player.input.down = false;
    }
})

// Start the  game loop
window.requestAnimationFrame(loop);
