import axios from "axios";

export default(url='https://localhost:7108/api') => {
    return axios.create({
        baseURL: url,
    })
}