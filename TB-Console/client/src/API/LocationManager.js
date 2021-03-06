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
  },
  addTag(tagRequest) {
    const authHeadersForm = createAuthHeaders();
    return fetch(`${baseUrl}/Locations/AddTags`, {
      method: 'POST',
      body: JSON.stringify(tagRequest),
      headers: authHeadersForm
    }).then(response => response.json());
  },
  deleteTag(locationId, tagId) {
    const authHeadersForm = createAuthHeadersForm();
    return fetch(`${baseUrl}/Locations/${locationId}/DeleteTag/${tagId}`, {
      method: 'DELETE',
      headers: authHeadersForm
    }).then(response => response.json());
  },
  editLocation(id, location) {
    const authHeader = createAuthHeadersForm();
    const options = {
      method: 'PUT',
      body: location,
      headers: authHeader
    };
    return fetch(`${baseUrl}Locations/${id}`, options).then(response =>
      response.json()
    );
  },
  deleteLocation(id) {
    const authHeadersForm = createAuthHeadersForm();
    return fetch(`${baseUrl}/Locations/${id}`, {
      method: 'DELETE',
      headers: authHeadersForm
    }).then(response => response.json());
  }
};
