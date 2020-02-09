import React, { Component } from 'react';
import { createAuthHeaders } from '../../API/userManager';
import LocationManager from '../../API/LocationManager';
import Mapper from '../map/LocationMapper';

class LocationList extends Component {
    state = {
        values: [],
        locations: [],
        address: '',
        tempAddress: ''
    };

    handleFieldChange = evt => {
        const stateToChange = {};
        stateToChange[evt.target.id] = evt.target.value;
        this.setState(stateToChange);
    };

    submitAddress = () => {
        //console.log(value);
        this.setState({
            address: this.state.tempAddress
        });
    };

    componentDidMount() {
        console.log('im locations list page', this.props);

        //ralative path

        LocationManager.getAllLocationsByAccount(this.props.accountId).then(
            data => {
                this.setState({ locations: data });
                console.log(data);
            }
        );
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
                <form className='modalContainer'>
                    <fieldset>
                        <div className='formgrid'>
                            <input
                                type='text'
                                required
                                onChange={this.handleFieldChange}
                                id='tempAddress'
                                placeholder='tempAddress'
                            />
                            <label htmlFor='animalName'>Name</label>

                            <button
                                type='button'
                                //disabled={this.state.loadingStatus}
                                onClick={this.submitAddress}
                            >
                                Submit
                            </button>
                        </div>
                    </fieldset>
                </form>
                <div>
                    {this.state.locations.map(location => (
                        <div key={location.locationId}>
                            <img
                                src={`https://localhost:5001${location.location.imageUrl}`}
                                alt='location image'
                            />
                            <div>{location.location.name}</div>
                            <div className={'mapWrapper'}>
                                <Mapper
                                    className={'map'}
                                    latitude={location.location.latitude}
                                    longitude={location.location.longitude}
                                    address={this.state.address}
                                />
                            </div>
                        </div>
                    ))}
                </div>
            </>
        );
    }
}

export default LocationList;
