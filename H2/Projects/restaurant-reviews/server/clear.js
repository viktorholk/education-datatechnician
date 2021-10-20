const db = require("./app/models")
require('dotenv').config('./.env')
db.mongoose
  .connect(process.env['MONGODB_URL'], {
    useNewUrlParser: true,
    useUnifiedTopology: true
  })
  .then(() => {
    console.log("Connected to the database!");
  })
  .catch(err => {
    console.log("Cannot connect to the database!", err);
    process.exit();
  });


const restaurant = db.restaurant
const data = [
    {
        title: 'Viborg Pizzaria',
        coordinates: [9.400473516446205, 56.44992292164805],
        reviews: [
            {
                rating: 5,
                description: "Yummers! After my meal, I was knocked into a food coma. The appetizers must be sprinkled with crack because I just craved for more and more. The decor was unique and incredible. The food was cooked to perfection. I would eat here every day if I could afford it!"
            },
            {
                rating: 4,
                description: "It was much better than I expected. The service was good for the most part but the waitress was a bit clueless. The entrees are simply to die for. Try out the huge selection of incredible appetizers. I had a satisfactory experience and will have to try it again."
            },
            {
                rating: 4,
                description: "This place was nearby and I decided to check it out. The entrees are simply to die for. After my meal, I was knocked into a food coma. I want to hire their decorator to furnish my apartment. Everything I tried was bursting with flavor. I had a satisfactory experience and will have to try it again."
            },
            {
                rating: 2,
                description: "I don't understand the hype about this place. The tofu dish tasted spongy and a bit bland. There were a lot of interesting decorations on the walls. There were bits of food stuck to my silverware. I would be hard pressed to come back."
            },
            {
                rating: 1,
                description: "Yuck! The whole place was just dirty. The kitchen screwed up my order completely, mixing it up with someone else's. It took almost an hour to get it corrected! The burger was so undercooked it started eating the lettuce. I wish I could put a sign out front that said ABANDON HOPE ALL YE WHO ENTER HERE!"
            },
            {
                rating: 1,
                description: "I really wanted to like this place. This place is very dumpy and in a serious need of a makeover. Seriously, how difficult is it to get a clean glass around here? I wish I could put a sign out front that said BEWARE OF DOG (FOOD)!"
            },
        ]
    },
    {
        title: "Restaurant Flammen",
        coordinates: [9.404401342833857, 56.45703023445463],
        reviews: [
            {
                rating: 3,
                description: "I had high hopes for this place. The chicken was undercooked. I was not very pleased to find out that the coffee wasn't local. The service was good for the most part but the waiter was a bit rude. The ambiance gives off an earthy feel-good vibe. I had a satisfactory experience and will have to try it again."
            },
            {
                rating: 4,
                description: "It was much better than I expected. The waiter was prompt and polite. The food was cooked to perfection. There were a lot of interesting decorations on the walls. After my meal, I was knocked into a food coma. 4 stars."
            },
            {
                rating: 2,
                description: "Dreadful place. The photos of the food were appetizing and palpable, but didn't live up to the hype. The waiter was mediocre at best. They need to get their act together before I set foot in this place again."
            },
        ]

    },
    {
        title: "Konge Buffet",
        coordinates: [9.363422871669206, 56.44636057132075],
        reviews: [
            {
                rating: 1,
                description: "I felt like this place wasn't trying at all. I threw up in my mouth a little when they brought me my food. I shouldn't have to pay good money to be served vegetables from a can. The dead flies on the window sill indicated to me that they don't do a good job cleaning and the flies found the food to be toxic. I found a dead cockroach on the floor of my booth. They can survive a nuclear explosion, but the entree was too much for them. I wish I could put a sign out front that said RUN AWAY!"
            },
            {
                rating: 5,
                description: "It was much better than I expected. The entrees are simply to die for. The desserts must be sprinkled with crack because I just craved for more and more. I'd give more than 5 stars if I could!"
            },
        ] 

    },
    {
        title: "Viborg Kebabhouse & Pizzaria",
        coordinates: [9.400054615847766, 56.44982262296175],
        reviews: [
            {
                rating: 4,
                description: "Decent place. This was one of the best mouth-watering burgers I've had grace my taste buds in a long time. The decor was unique and incredible. It failed to meet the 5-star experience because the table was a little sticky."
            },
            {
                rating: 2,
                description: "Meh. Some of my favorite dishes are no longer available. I was not very pleased to find out that the coffee wasn't fair trade. The ambiance gives off an earthy feel-good vibe. I had a less than satisfactory experience and will probably not be here again."
            },
            {
                rating: 1,
                description: "I can't believe this place is still in business! The food sucked. The service sucked. Everything sucked. The whole place was just dirty. The burger was so undercooked it started eating the lettuce. I gave 1 star because I couldn't give 0."
            }
        ] 

    },
    {
        title: "ILD.PIZZA",
        coordinates: [9.384290456326521, 56.44902052483915],
        reviews: [
            {
                rating: 4,
                description: "Decent place. I found the ambiance to be very charming. Everything was mostly decadent. The waiter did an excellent job. The appetizers must be sprinkled with crack because I just craved for more and more. 4 stars of satisfaction."
            },
            {
                rating: 5,
                description: "I stumbled on this undiscovered gem right in our neighboorhood. Everything was simply decadent. Make sure to save room for dessert, because that was the best part of the meal! I would eat here every day if I could afford it!"
            },
            {
                rating: 2,
                description: "Meh. I was not very pleased to find out that the coffee wasn't fair trade. The whole place was just dirty. I would be hard pressed to come back."
            }
        ] 
    },
    {
        title: "Havanna Pizza Bar",
        coordinates: [8.6182950807406071, 56.35784201378391],
        reviews: [
            {
                rating: 5,
                description: "My taste buds are still dancing from our last visit! The decor was unique and incredible. After my meal, I was knocked into a food coma. The appetizers must be sprinkled with crack because I just craved for more and more. The food was cooked to perfection. 5 stars!"
            },
            {
                rating: 4,
                description: "It was much better than I expected. Everything was mostly decadent. After my meal, I was knocked into a food coma. I removed a star because the busboy kept looking at me funny the whole time."
            },
            {
                rating: 3,
                description: "When I walked in, I really wasn't expecting much. The waitress was mediocre at best. I found the entrees to be somewhat agreeable to my personal flavor-profile. The menu didn't match the one on their website. I had a satisfactory experience and will have to try it again."
            },
            {
                rating: 5,
                description: "I stumbled on this undiscovered gem right in our neighboorhood. I found the ambiance to be very charming. Everything was simply decadent. I'm definitely coming back for more!"
            },
            {
                rating: 5,
                description: "This is one of my favorite places. The food is simply to die for. Try out the huge selection of incredible appetizers. I found the ambiance to be very charming. I'd give more than 5 stars if I could!!"
            },
        ] 
    },
]
restaurant.deleteMany().then(() => {
    restaurant.insertMany(data).then( () => {
        for (const restaurant of data) {
            console.log(`Added ${restaurant.title}`)
        }
    })
})



