import { load } from '@2gis/mapgl';
import React, {useEffect, useRef, useState} from "react";
import { fetchNearbyPlaces } from '/src/services/Api.js'
import { getMarkerPopupHTML } from './MarkerPopup';
import { useNavigate } from 'react-router-dom';


export const MapGL = () => {
    const navigate = useNavigate();
    const mapRef = useRef(null);
    const mapInstance = useRef(null);
    const [markers, setMarker] = useState(new Map());
    const currentPopup = useRef(null);

    const addItemToMap = (key, value) => {
        setMarker((prevMap) => new Map(prevMap.set(key, value)));
    };

    useEffect(() => {
        let mounted = true;

        const initMap = (position) => {
            load().then((mapglAPI) => {
                if (!mounted || !mapRef.current) return;

                const center = position
                    ? [position.coords.longitude, position.coords.latitude]
                    : [40.5433, 64.5401]; // Fallback координаты


                mapInstance.current = new mapglAPI.Map(mapRef.current, {
                    center,
                    zoom: 15,
                    key: 'a1e81da5-7c22-4dc9-81ad-c1194c9e16a6',
                });

                // Добавляем маркер текущего местоположения
                if (position) {
                    new mapglAPI.Marker(mapInstance.current, {
                        coordinates: center,
                        icon: 'https://docs.2gis.com/img/mapgl/marker.svg'
                    });

                const loadAndDisplayMarkers = async () => {
                    try {
                        const places = await fetchNearbyPlaces(
                            position.coords.latitude,
                            position.coords.longitude
                        );

                        if (Array.isArray(places)) {
                            places.forEach((data) => {
                                const coordinates = [data.coordinate.longitude, data.coordinate.latitude];

                                // Создаем маркер
                                const marker = new mapglAPI.Marker(mapInstance.current, {
                                    coordinates,
                                    icon: 'https://docs.2gis.com/img/dotMarker.svg'
                                });

                                // Добавляем обработчик клика
                                marker.on('click', (e) => {
                                    // Закрываем предыдущий попап
                                    if (currentPopup.current) {
                                        currentPopup.current.destroy();
                                    }

                                    const data = markers.get(e.targetData);
                                    console.log(data);

                                    const popupContainer = document.createElement('div');
                                    popupContainer.innerHTML = getMarkerPopupHTML({
                                        id: data.id, // Добавляем ID места
                                        title: data.title,
                                        description: data.description,
                                        image: data.images?.[0]
                                    });

                                    currentPopup.current = new mapglAPI.HtmlMarker(mapInstance.current, {
                                        coordinates: [data.coordinate.longitude, data.coordinate.latitude],
                                        html: popupContainer,
                                        offset: [0, -40]
                                    });

                                    // Обработчик закрытия попапа
                                    const closeBtn = popupContainer.querySelector('.popup-close-btn');
                                    if (closeBtn) {
                                        closeBtn.addEventListener('click', () => {
                                            if (currentPopup.current) {
                                                currentPopup.current.destroy();
                                                currentPopup.current = null;
                                            }
                                        });
                                    }

                                    // Обработчик кнопки "Подробнее"
                                    const detailsBtn = popupContainer.querySelector('.details-btn');
                                    if (detailsBtn) {
                                        detailsBtn.addEventListener('click', () => {

                                            if (currentPopup.current) {
                                                currentPopup.current.destroy();
                                                currentPopup.current = null;
                                            }
                                            console.log(data.id);
                                            navigate('/place', { state: {id: data.id}})
                                        });
                                    }
                                });

                                addItemToMap(marker, data);
                            });
                        }
                    } catch (error) {
                        console.error('Ошибка при загрузке маркеров:', error);
                    }
                };

                loadAndDisplayMarkers();
                }

            }).catch(error => {
                console.error('Ошибка загрузки карты:', error);
            });
        };

        // Пробуем получить геолокацию
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => initMap(position),
                (error) => {
                    console.warn('Ошибка геолокации:', error);
                    initMap(null); // Используем fallback координаты
                },
                { enableHighAccuracy: true, timeout: 5000 }
            );
        } else {
            console.warn('Геолокация не поддерживается браузером');
            initMap(null);
        }

        return () => {
            mounted = false;
            if (mapInstance.current) {
                mapInstance.current.destroy();
                mapInstance.current = null;
            }
        };
    }, []);

    return (
        <div style={{ width: '100%', height: '100vh', position: 'relative' }}>
            <div
                ref={mapRef}
                style={{
                    width: '100%',
                    height: '100%'
                }}
            />
        </div>
    );
};