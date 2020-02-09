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
import GeoSearch from './Geosearch';
import { OpenStreetMapProvider } from 'leaflet-geosearch';

const myIcon = L.icon({
    iconUrl: '/images/markers/icon1.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    tooltipAnchor: [15, -30],
    shadowUrl: '/images/markers/shadow.png',
    shadowSize: [30, 41],
    shadowAnchor: [9, 41],
    popupAnchor: [0, -45]
});

export default class LocationMapper extends Component {
    state = {
        lat: '',
        lng: '',
        zoom: 8
    };

    //function for storing click events on geosearch and click to add markers
    storeGeocode = (e, obj) => {
        console.log('yaya got dem O-B-Js', obj);
    };

    markerFocus = e => {
        console.log('got the deets', e);
    };

    dragEnd = e => {
        console.log(e.target.getLatLng().lat);
        console.log(e.target.getLatLng().lng);
    };

    handleFieldChange = evt => {
        const stateToChange = {};
        stateToChange[evt.target.id] = evt.target.value;
        this.setState(stateToChange);
    };

    componentDidUpdate(prevProps) {
        // Typical usage (don't forget to compare props):
        if (this.props.address !== prevProps.address) {
            console.log(this.props);

            const map = this.leafletMap.leafletElement;

            var query_addr = this.props.address;

            const provider = new OpenStreetMapProvider();
            var query_promise = provider.search({
                query: query_addr
            });

            query_promise.then(
                value => {
                    for (var i = 0; i < 1; i++) {
                        // Success!
                        var x_coor = value[i].x;
                        var y_coor = value[i].y;
                        var label = value[i].label;
                        console.log('value', value);
                        var marker = L.marker([y_coor, x_coor], {
                            icon: myIcon,
                            draggable: true
                        })
                            .on('dragend', function(e) {
                                console.log(marker.getLatLng().lat);
                                console.log(marker.getLatLng().lng);
                            })
                            .addTo(map); // CAREFULL!!! The first position corresponds to the lat (y) and the second to the lon (x)
                        marker
                            .bindPopup('<b>Found location</b><br>' + label)
                            .openPopup();
                        map.fitBounds(value[i].bounds);
                    }
                },
                reason => {
                    console.log(reason); // Error!
                }
            );

            // map.on('click', e => {
            //     geocoder.reverse(
            //         e.latlng,
            //         map.options.crs.scale(map.getZoom()),
            //         results => {
            //             var r = results[0];
            //             console.log('reverse geocode results', r);
            //             if (r) {
            //                 if (marker) {
            //                     marker
            //                         .setLatLng(r.center)
            //                         .setPopupContent(r.html || r.name);
            //                     // .openPopup();
            //                 } else {
            //                     marker = L.marker(r.center, {
            //                         icon: myIcon,
            //                         draggable: true
            //                     })
            //                         .bindTooltip(r.name, { className: 'toolTip' })
            //                         .addTo(map)
            //                         //.on('click', e => this.storeGeocode(e, r));
            //                         .on('dragend', function(e) {
            //                             console.log(marker.getLatLng().lat);
            //                             console.log(marker.getLatLng().lng);
            //                         });
            //                     // .openPopup();
            //                 }
            //             }
            //         }
            //     );
            // });
        }
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
        const position = [this.props.latitude, this.props.longitude];
        console.log('psoition', position);
        return (
            <>
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
                    {/* <GeoSearch
                    ref={m => {
                        this.leafletGeo = m;
                    }}
                    storeGeocode={this.storeGeocode}
                    props={this.props}
                /> */}
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
                        ondragend={e => this.dragEnd(e)}
                    >
                        <Tooltip>{'hiya'}</Tooltip>
                    </Marker>
                    {/* </FeatureGroup> */}
                </Map>
            </>
        );
    }
}