import React, { Component } from 'react';
import LocationManager from '../../API/LocationManager';
import './Location.css';
import SwitchView from './SwitchView';
import LocationCard from './LocationCard';
import LocationTable from './LocationTable';

class LocationList extends Component {
    state = {
        locations: [],
        cardView: true
    };

    getLocations = () => {
        LocationManager.getAllLocationsByAccount(this.props.accountId).then(
            data => {
                this.setState({ locations: data });
                console.log(data);
            }
        );
    };

    changeView = () => {
        if (this.state.cardView === true) {
            this.setState({
                cardView: false
            });
        } else {
            this.setState({
                cardView: true
            });
        }
    };

    componentDidMount() {
        // console.log('im locations list page', this.props);

        this.getLocations();
    }

    render() {
        return (
            <>
                <section className='section-content'>
                    <div className='addViewRow'>
                        {/* <FormDialog {...this.props} getData={this.getData} /> */}
                        <SwitchView changeView={this.changeView} />
                    </div>
                </section>
                {this.state.cardView ? (
                    <div className='container-cards'>
                        {this.state.locations.map(locationDetails => (
                            <LocationCard
                                key={locationDetails.location.id}
                                locationDetails={locationDetails}
                                getData={this.getData}
                                {...this.props}
                                cardView={this.state.cardView}
                            />
                        ))}
                    </div>
                ) : (
                    <div className='container-table'>
                        <LocationTable
                            locations={this.state.locations}
                            getData={this.getData}
                            {...this.props}
                        />
                    </div>
                )}
            </>
        );
    }
}

export default LocationList;

// {this.state.locations.map(location => (
//     <div key={location.locationId}>
//         <img
//             src={`https://localhost:5001${location.location.imageUrl}`}
//             alt='location image'
//         />
//         <div>{location.location.name}</div>
//         <div className={'mapWrapper'}>
//             <Mapper
//                 className={'map'}
//                 latitude={location.location.latitude}
//                 longitude={location.location.longitude}
//                 address={this.state.address}
//             />
//         </div>
//     </div>
// ))}
