import { createAuthHeaders } from './userManager';

const baseUrl = '/api/v1/';

const authHeader = createAuthHeaders();

export default {
    getAllLocationsByAccount(id, search, tag, category, active) {
        const start = Date.now();
        console.log('start fetch', start);
        return fetch(
            `${baseUrl}Locations/byAccount/${id}?search=${search}&tag=${tag}&category=${category}&isActive=${active}`,
            {
                headers: authHeader
            }
        ).then(response => {
            console.log('Time to response', Date.now() - start);
            return response.json();
        });
    },
    getLocationById(id) {
        return fetch(`${baseUrl}Locations/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
