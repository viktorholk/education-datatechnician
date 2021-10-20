module.exports = mongoose => {
    const Restaurant = mongoose.model(
      "restaurant",
      mongoose.Schema(
        {
          title: String,
          coordinates: [],
          reviews: [
            {
              rating: Number,
              description: String
            }    
          ]
        }
      )
    );
  
    return Restaurant;
  };