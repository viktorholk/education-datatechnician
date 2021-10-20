module.exports = app => {
    const restaurant = require("../controllers/restaurant.controller.js");
  
    var router = require("express").Router();
  
    // Retrieve all restaurant
    router.get("/", restaurant.findAll);

    // Update a Tutorial with id
    router.put("/:id", restaurant.update);
  
    app.use('/api/restaurants', router);
  };