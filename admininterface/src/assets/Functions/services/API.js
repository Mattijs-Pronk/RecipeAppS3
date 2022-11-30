import axios from "axios";
import { HostUrl } from "./BaseUrls";


export default(url= HostUrl + '/api') => {
    return axios.create({
        baseURL: url,
    })
}