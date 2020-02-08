import React, { Component } from 'react';
import { Map, TileLayer, Marker, Popup, Tooltip } from 'react-leaflet';
import Token from '../../Token';
import L from 'leaflet';
import Geosearch from '../Map/Geosearch';

const myIcon = L.icon({
    iconUrl: '/images/markers/icon1.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    tooltipAnchor: [15, -30],
    shadowUrl: '/images/markers/shadow.png',
    shadowSize: [30, 41],
    shadowAnchor: [9, 41]
});

export default class Mapper extends Component {
    state = {
        lat: 0,
        lng: 0,
        zoom: 13
    };

    //function for storing click events on geosearch and click to add markers
    storeGeocode = (e, obj) => {
        console.log('yaya got dem O-B-Js', obj);
    };

    markerFocus = (e, obj) => {
        console.log('got the deets', obj);
    };

    componentDidMount() {
        console.log(this.props);
        this.setState({
            lat: this.props.latitude,
            lng: this.props.longitude
        });
        const map = this.leafletMap.leafletElement;
        const geocoder = L.Control.Geocoder.mapbox(Token.MB);
        let marker;

        map.on('click', e => {
            geocoder.reverse(
                e.latlng,
                map.options.crs.scale(map.getZoom()),
                results => {
                    var r = results[0];
                    console.log('reverse geocode results', r);
                    if (r) {
                        if (marker) {
                            marker
                                .setLatLng(r.center)
                                .setPopupContent(r.html || r.name);
                            // .openPopup();
                        } else {
                            marker = L.marker(r.center, { icon: myIcon })
                                .bindTooltip(r.name, { className: 'toolTip' })
                                .addTo(map)
                                .on('click', e => this.storeGeocode(e, r));
                            // .openPopup();
                        }
                    }
                }
            );
        });
    }

    getCoord = e => {
        const lat = e.latlng.lat;
        const lng = e.latlng.lng;
        // L.marker([lat, lng])
        // 	.bindPopup('this is a smashing place')
        // 	.addTo(this.map);
        console.log(lat, lng);
    };

    render() {
        const Atoken = `https://api.mapbox.com/styles/v1/jerodis/cjslgf0z045tb1fqdutmd3q71/tiles/256/{z}/{x}/{y}@2x?access_token=${Token.MB}`;
        const position = [this.state.lat, this.state.lng];
        console.log('psoition', position);
        return (
            <Map
                center={position}
                zoom={this.state.zoom}
                maxZoom={18}
                className='map'
                ref={m => {
                    this.leafletMap = m;
                }}
                onClick={this.getCoord}
            >
                <TileLayer
                    attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    url={Atoken}
                />

                <Marker
                    position={position}
                    anchor='bottom'
                    icon={myIcon}
                    // onMouseEnter={this.onMarkerClick.bind(this, location)}s
                    // onMouseLeave={this.onMarkerLeave}
                >
                    <Tooltip>{'hiya'}</Tooltip>
                </Marker>
            </Map>
        );
    }
}
