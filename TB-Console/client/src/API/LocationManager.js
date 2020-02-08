import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api/';

const authHeader = createAuthHeaders();

export default {
    getAllLocations(id) {
        return fetch(`${baseUrl}/Locations/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
