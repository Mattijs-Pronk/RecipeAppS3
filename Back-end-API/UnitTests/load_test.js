import http from 'k6/http';
import { sleep } from 'k6';

//om de test te runnen:
//rechtermuis klik op Back-end-APITests, Open in terminal, "k6 run load_test.js"

export let options = {
    stages: [
        { duration: '30s', target: 20 }, //binnen 30sec naar 20 users.
        { duration: '2m', target: 20 }, //2min lang op 20 users.
        { duration: '30s', target: 0 } //binnen 30sec terug naar 0 users.
    ],
    thresholds: {
        http_req_duration: ['p(90)<2500'], //90% van de requesten moeten onder 2500ms zitten. threshold is zo hoog omdat er bytes[] uit de db moeten worden gehaald.
        http_req_failed: ['rate<0.05'] //aantal gefaalde requests.
    },
};

const API_BASE_URL = 'https://localhost:8000/api/';

export default () => {
    http.get(API_BASE_URL + 'Recipe/getallaccepted');
    http.get(API_BASE_URL + 'Recipe/getallonhold');
    http.get(API_BASE_URL + 'Recipe/getalldeclined');
    //sleep om een request per seconden te krijgen.
    sleep(1);
};
