import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { fetchGetPlace } from '/src/services/Api.js';
import './PlacePage.css'; // Подключаем стили

export const PlacePage = () => {
    const { state } = useLocation();
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

    if (loading) return (
        <div className="place-loading">
            <div className="spinner"></div>
            <p>Загружаем информацию о месте...</p>
        </div>
    );

    if (error) return (
        <div className="place-error">
            <div className="error-icon">!</div>
            <h2>Произошла ошибка</h2>
            <p>{error}</p>
            <button onClick={() => window.location.reload()} className="retry-button">
                Попробовать снова
            </button>
        </div>
    );

    if (!place) return (
        <div className="place-not-found">
            <h2>Место не найдено</h2>
            <p>Запрошенное место не существует или было удалено</p>
            <button onClick={() => navigate(-1)} className="back-button">
                Вернуться назад
            </button>
        </div>
    );

    return (
        <div className="place-container">
            <button onClick={() => navigate(-1)} className="back-button">
                <span className="arrow">←</span> Назад к карте
            </button>

            <div className="place-header">
                <div className="place-title-wrapper">
                    <h1 className="place-title">{place.title}</h1>
                    <div className="place-coordinates">
                        <span className="coordinate-badge">
                            <i className="pin-icon">📍</i>
                            {place.coordinate.latitude.toFixed(6)}, {place.coordinate.longitude.toFixed(6)}
                        </span>
                    </div>
                </div>
            </div>

            <div className="place-content">
                {place.description && (
                    <div className="place-description-section">
                        <h2 className="section-title">
                            <i className="icon">📝</i> Описание
                        </h2>
                        <p className="description-text">{place.description}</p>
                    </div>
                )}

                {place.images?.length > 0 && (
                    <div className="place-gallery-section">
                        <h2 className="section-title">
                            <i className="icon">📷</i> Фотографии
                        </h2>
                        <div className="gallery-grid">
                            {place.images.map((img, index) => (
                                <div key={index} className="gallery-item">
                                    <img
                                        src={`data:image/jpeg;base64,${img}`}
                                        alt={`${place.title} ${index + 1}`}
                                        className="gallery-image"
                                    />
                                </div>
                            ))}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default PlacePage;