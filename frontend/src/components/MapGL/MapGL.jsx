import { load } from '@2gis/mapgl';
import React, { useEffect, useRef } from "react";

export const MapGL = () => {
    const mapRef = useRef(null);
    const mapInstance = useRef(null);

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
                        icon: 'https://docs.2gis.com/img/dotMarker.svg',
                        label: 'Вы здесь'
                    });
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