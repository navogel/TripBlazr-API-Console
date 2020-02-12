import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api';

const authHeader = createAuthHeaders();

export default {
    getAllTags(id) {
        const authHeader = createAuthHeaders();
        return fetch(`${baseUrl}/Tags/ByAccount`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
