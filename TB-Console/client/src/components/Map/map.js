import React, { Component } from 'react';
import { Map, TileLayer, Marker, Tooltip, ZoomControl } from 'react-leaflet';
import Token from '../../Token';
import L from 'leaflet';
import GeoSearch from './GeoSearch';
import Control from 'react-leaflet-control';




export default class Map extends Component {
	state = {
		lat: '',
		lng: '',
		zoom: 14,
		light: true,
		searchResults: [],
		tripView: true,
		searchTerm: '',
		searchRange: 8000,
		stars: '3',
		recievedTrip: false,
		mapLoaded: false
	};

