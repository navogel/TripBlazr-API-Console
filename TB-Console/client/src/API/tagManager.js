import { createAuthHeaders } from '../API/userManager';

const baseUrl = '/api';

export default {
  getAllTags(id) {
    const authHeader = createAuthHeaders();
    return fetch(`${baseUrl}/Tags/ByAccount/${id}`, {
      headers: authHeader
    }).then(response => response.json());
  }
};
