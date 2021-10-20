import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:8081/api",
    headers: {
      "Content-type": "application/json"
    }
  });

class RestaurantDataService {
  getAll() {
    return http.get("/restaurants");
  }
  update(id, data) {
    return http.put(`/restaurants/${id}`, data);
  }
}

export default new RestaurantDataService();