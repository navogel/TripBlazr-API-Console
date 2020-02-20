import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api';

const noAuth = { response: 'not authorized' };

export default {
  getAllAccounts() {
    const authHeader = createAuthHeaders();
    return fetch(`${baseUrl}/Accounts`, {
      headers: authHeader
    }).then(response => {
      if (response.status === 401) {
        return noAuth;
      }
      response.json();
    });
  },
  getAccountById(id) {
    const authHeader = createAuthHeaders();
    return fetch(`${baseUrl}/Accounts/${id}`, {
      headers: authHeader
    }).then(response => {
      //console.log(response.status);
      if (response.status === 401) {
        return noAuth;
      }
      response.json();
    });
  }
};
