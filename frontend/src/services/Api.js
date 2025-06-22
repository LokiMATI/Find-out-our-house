import axios from 'axios';

const API_BASE = 'http://localhost:5089/api';

export const fetchNearbyPlaces = async (lat, lng, radius = 1) => {
    const response = await axios.get(
        `${API_BASE}/Places/near?latitude=${lat}&longitude=${lng}&radius=${radius}`);
    return response.data;
};

export const fetchPlaces = async () => {
    const response = await axios.get(`http://localhost:5089/api/Places`);
    return response.data;
};

export const fetchGetPlace = async (id) => {
    const response = await axios.get(`${API_BASE}/Places/${id}`);
    return response.data;
};

export const createPlace = async (place) => {
    const response = await axios.post(`${API_BASE}/Places`, place);
    return response;
}