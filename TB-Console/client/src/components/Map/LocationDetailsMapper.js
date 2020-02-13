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
import Snackbar from '@material-ui/core/Snackbar';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import ErrorIcon from '@material-ui/icons/Error';
import SnackbarContent from '@material-ui/core/SnackbarContent';

let marker = L.marker();

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

export default class LocationDetailsMapper extends Component {
    state = {
        lat: '',
        lng: '',
        zoom: 17,
        cityLat: '',
        cityLng: '',
        snackOpen: false
    };

    handleSnackClick = () => {
        this.setState({ snackOpen: true });
        //console.log('snackery');
    };

    handleSnackClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        this.setState({ snackOpen: false });
    };

    dragEnd = e => {
        // console.log(e.target.getLatLng().lat);
        // console.log(e.target.getLatLng().lng);
        this.props.grabCoordsFromPin(
            e.target.getLatLng().lat,
            e.target.getLatLng().lng
        );
    };

    componentDidUpdate(prevProps) {
        // Typical usage (don't forget to compare props):
        if (this.props.mapAddress !== prevProps.mapAddress) {
            const map = this.leafletMap.leafletElement;
            //console.log('mapAddress props update', this.props.mapAddress);
            var query_addr = this.props.mapAddress;

            const provider = new OpenStreetMapProvider();
            var query_promise = provider.search({
                query: query_addr
            });

            query_promise.then(value => {
                // console.log('search results', value);
                if (value.length != 0) {
                    let posLat;
                    let posLng;
                    var x_coor = value[0].x;
                    var y_coor = value[0].y;
                    var label = value[0].label;
                    this.props.grabCoordsFromPin(y_coor, x_coor);
                    if (marker != undefined) {
                        map.removeLayer(marker);
                    }
                    marker = L.marker([y_coor, x_coor], {
                        icon: myIcon,
                        draggable: true
                    })
                        .on('dragend', e => {
                            this.dragEnd(e);
                            //console.log('changed', posLat, posLng);

                            //this.props.grabCoordsFromPin(posLat, posLng);
                        })
                        .addTo(map); // CAREFULL!!! The first position corresponds to the lat (y) and the second to the lon (x)
                    marker
                        .bindPopup('<b>Found location</b><br>' + label)
                        .openPopup();
                    map.fitBounds(value[0].bounds);

                    return value;
                } else {
                    this.handleSnackClick();
                    return value;
                }
            });
        }
    }

    render() {
        const Atoken = `https://api.mapbox.com/styles/v1/jerodis/ck24x2b5a12ro1cnzdopvyw08/tiles/256/{z}/{x}/{y}@2x?access_token=${Token.MB}`;
        const position = [this.props.cityLat, this.props.cityLng];
        // console.log('snack', this.state.snackOpen);
        return (
            <>
                <Map
                    center={position}
                    zoom={this.state.zoom}
                    maxZoom={18}
                    className='locationMap'
                    ref={m => {
                        this.leafletMap = m;
                    }}
                    onClick={this.getCoord}
                >
                    <TileLayer url={Atoken} />
                    <Marker
                        position={position}
                        anchor='bottom'
                        icon={myIcon}
                        draggable={true}
                        // onMouseEnter={this.onMarkerClick.bind(this, location)}s
                        // onMouseLeave={this.onMarkerLeave}
                        ondragend={e => this.dragEnd(e)}
                    >
                        <Tooltip>{'hiya'}</Tooltip>
                    </Marker>
                </Map>

                <Snackbar
                    anchorOrigin={{
                        vertical: 'top',
                        horizontal: 'left'
                    }}
                    open={this.state.snackOpen}
                    autoHideDuration={5000}
                    onClose={this.handleSnackClose}
                    ContentProps={{
                        'aria-describedby': 'message-id'
                    }}

                    //className='snackWarning'
                >
                    <SnackbarContent
                        style={{
                            backgroundColor: '#c71c3e'
                        }}
                        message={
                            <span id='message-id'>
                                <IconButton
                                    key='close'
                                    aria-label='Close'
                                    color='inherit'
                                >
                                    <ErrorIcon />
                                </IconButton>
                                <b>
                                    Oops there were no results from that name +
                                    address. Try a different search or choose a
                                    nearby location and drag the marker!
                                </b>
                            </span>
                        }
                        action={[
                            <IconButton
                                key='close'
                                aria-label='Close'
                                color='inherit'
                                //className={classes.close}
                                onClick={this.handleSnackClose}
                            >
                                <CloseIcon />
                            </IconButton>
                        ]}
                    />
                </Snackbar>
            </>
        );
    }
}
