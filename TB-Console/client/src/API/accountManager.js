import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api';

export default {
    getAllAccounts() {
        const authHeader = createAuthHeaders();
        return fetch(`${baseUrl}/Accounts`, {
            headers: authHeader
        }).then(response => response.json());
    },
    getAccountById(id) {
        const authHeader = createAuthHeaders();
        return fetch(`${baseUrl}/Accounts/${id}`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
