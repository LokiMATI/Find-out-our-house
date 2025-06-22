import React, { useState, useEffect } from 'react';
import {useLocation, useNavigate} from 'react-router-dom';
import { fetchGetPlace } from '/src/services/Api.js';

export const PlacePage = () => {
    const {state} = useLocation();
    const { id } = state;
    const navigate = useNavigate();
    const [place, setPlace] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchPlace = async () => {
            try {
                setLoading(true);

                const response = await fetchGetPlace(id);
                setPlace(response);
            } catch (err) {
                setError(err.message);
                console.error('Ошибка загрузки данных:', err);
            } finally {
                setLoading(false);
            }
        };

        fetchPlace();
    }, [id]);

    if (loading) return <div className="loading">Загрузка...</div>;
    if (error) return <div className="error">Ошибка: {error}</div>;
    if (!place) return <div className="error">Место не найдено</div>;

    return (
        <div className="place-page">
            <button
                onClick={() => navigate(-1)}
                className="back-button"
            >
                &larr; Назад
            </button>

            <div className="place-header">
                <h1>{place.title}</h1>
                <div className="coordinates">
                    Широта: {place.coordinate.latitude.toFixed(6)}, Долгота: {place.coordinate.longitude.toFixed(6)}
                </div>
            </div>

            {place.description && (
                <div className="place-description">
                    <h2>Описание</h2>
                    <p>{place.description}</p>
                </div>
            )}

            {place.images?.length > 0 && (
                <div className="place-gallery">
                    <h2>Фотографии</h2>
                    <div className="images-grid">
                        {place.images.map((img, index) => (
                            <div key={index} className="image-wrapper">
                                <img
                                    src={`data:image/jpeg;base64,${img}`}
                                    alt={`${place.title} ${index + 1}`}
                                />
                            </div>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export default PlacePage;