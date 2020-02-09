import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api';

const authHeader = createAuthHeaders();

export default {
    getAllAccounts() {
        return fetch(`${baseUrl}/Accounts`, {
            headers: authHeader
        }).then(response => response.json());
    }
};
