import axios from 'axios';

const API_BASE = 'http://localhost:5089/api';

export const fetchNearbyPlaces = async (lat, lng, radius = 1) => {
    const response = await axios.get(
        `${API_BASE}/Places/near?latitude=${lat}&longitude=${lng}&radius=${radius}`
    );
    console.log(response.data);
    return response.data;
};