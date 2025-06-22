import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 1000,
    duration: '10s',
};

export default function () {
    const res = http.get('http://172.17.0.1:5164/api/Course/Popular');
    const ok = check(res, {
        'status is 200': (r) => r.status === 200,
        'response is array': (r) => r.status === 200 && Array.isArray(r.json()),
    });
    if (!ok) {
        console.log(`Fault: status=${res.status}, body=${res.body}`);
    }
    sleep(1);
}

