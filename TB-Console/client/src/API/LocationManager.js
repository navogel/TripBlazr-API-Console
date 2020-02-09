import { createAuthHeaders } from './userManager';

const baseUrl = '/api/';

const authHeader = createAuthHeaders();

export default {
    getAllLocationsByAccount(id) {
        return fetch(`${baseUrl}v1/Locations/byAccount/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    },
    getLocation(id) {
        return fetch(`${baseUrl}v1/Locations/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
