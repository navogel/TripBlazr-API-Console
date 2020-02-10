import { createAuthHeaders } from './userManager';

const baseUrl = '/api/v1/';

const authHeader = createAuthHeaders();

export default {
    getAllLocationsByAccount(id, search, tag, category, active) {
        return fetch(
            `${baseUrl}Locations/byAccount/${id}?search=${search}&tag=${tag}&category=${category}&isActive=${active}`,
            {
                headers: authHeader
            }
        ).then(response => response.json());
    },
    getLocation(id) {
        return fetch(`${baseUrl}Locations/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
