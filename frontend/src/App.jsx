import { MapGL } from './components/MapGL/MapGL.jsx'
import { PlacePage } from './components/Place/PlacePage.jsx'
import React from "react";
import {Route, Routes} from "react-router-dom";

const App = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<MapGL/>} />
                <Route path="/place" element={<PlacePage/>} />
            </Routes>
        </>
    );
};

export default App;