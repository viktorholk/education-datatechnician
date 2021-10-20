const db = require("../models");
const Restaurant = db.restaurant;
// Retrieve all Tutorials from the database.

exports.findAll = (req, res) => {
    Restaurant.find()
    .then(data => {
      res.send(data);
    })
    .catch(err => {
      res.status(500).send({
        message:
          err.message || "Some error occurred while retrieving tutorials."
      });
    });
};

// Update a Tutorial by the id in the request
exports.update = (req, res) => {
    
    // const data = JSON.parse(req.body) 
    const id        = req.params.id
    const rating    = req.body

    Restaurant.findOneAndUpdate(
        { _id: id },
        { $push: { reviews: rating }},
        { new: true },
        ( error, doc ) => {
            if (error) {
                res.status(500).send({
                    message: error
                })
            } else {
                res.status(200).send(doc)
            }
        }
    )
};
