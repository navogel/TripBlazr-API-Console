import { createAuthHeaders, createAuthHeadersForm } from './userManager';

const baseUrl = '/api/v1/';

export default {
    getAllLocationsByAccount(id, search, tag, category, active) {
        const authHeader = createAuthHeaders();
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
        const authHeader = createAuthHeaders();
        return fetch(`${baseUrl}Locations/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    },
    createLocation(location) {
        const authHeader = createAuthHeadersForm();
        const options = {
            method: 'POST',
            body: location,
            headers: authHeader
        };
        // if (options && options.headers) {
        //     delete options.headers['Content-Type'];
        // }
        return fetch(`${baseUrl}Locations`, options).then(response =>
            response.json()
        );
    },
    createPrimaryCategory(locationId, categoryId) {
        const authHeader = createAuthHeaders();
        return fetch(
            `${baseUrl}Locations/${locationId}/AddCategory/${categoryId}?isPrimary=true`,
            {
                method: 'POST',
                headers: authHeader
            }
        ).then(response => response.json());
    },
    toggleInactive(id) {
        const authHeader = createAuthHeaders();
        return fetch(`${baseUrl}Locations/${id}/isActive`, {
            method: 'PUT',
            headers: authHeader
        }).then(response => response.json());
    }
};
