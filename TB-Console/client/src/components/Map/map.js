import React, { Component } from 'react';
import {
    Map,
    TileLayer,
    Marker,
    Popup,
    Tooltip,
    FeatureGroup
} from 'react-leaflet';
import Token from '../../Token';
import L from 'leaflet';
import GeoSearch from '../Map/Geosearch';

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

    markerFocus = e => {
        console.log('got the deets', e);
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
        const Atoken = `https://api.mapbox.com/styles/v1/jerodis/ck24x2b5a12ro1cnzdopvyw08/tiles/256/{z}/{x}/{y}@2x?access_token=${Token.MB}`;
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
                <GeoSearch
                    ref={m => {
                        this.leafletGeo = m;
                    }}
                    storeGeocode={this.storeGeocode}
                    props={this.props}
                />
                <TileLayer
                    attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    url={Atoken}
                />
                {/* <FeatureGroup
                    ref='features'
                    onAdd={this.onFeatureGroupAdd}
                    // onClick={e => this.storeGeocode(e)}
                > */}
                <Marker
                    position={position}
                    anchor='bottom'
                    icon={myIcon}
                    draggable={true}
                    // onMouseEnter={this.onMarkerClick.bind(this, location)}s
                    // onMouseLeave={this.onMarkerLeave}
                    onClick={e => this.markerFocus(e)}
                >
                    <Tooltip>{'hiya'}</Tooltip>
                </Marker>
                {/* </FeatureGroup> */}
            </Map>
        );
    }
}
