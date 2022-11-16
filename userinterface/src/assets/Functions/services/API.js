import axios from "axios";

//apilink aanpassen als deze niet op docker runt
//apilink wanneer op docker: http://localhost:8000/api
//apilink wanneer niet op docker: https://localhost:8000/api
export default(url='http://localhost:8000/api') => {
    return axios.create({
        baseURL: url,
    })
}