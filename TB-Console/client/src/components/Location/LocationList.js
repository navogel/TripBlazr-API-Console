import React, { Component } from 'react';
import LocationManager from '../../API/LocationManager';
import accountManager from '../../API/accountManager';
import './Location.css';
import SwitchView from './SwitchView';
import LocationCard from './LocationCard';
import LocationTable from './LocationTable';
import AddLocationForm from './AddLocationForm';
import DialogTitle from '@material-ui/core/DialogTitle';
import CircularProgress from '@material-ui/core/CircularProgress';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';

class LocationList extends Component {
    state = {
        locations: [],
        cardView: false,
        search: '',
        tag: '',
        category: '',
        isActive: '',
        //account info
        city: '',
        cityLat: '',
        cityLng: '',
        accountName: '',
        accountId: '',
        loading: true,
        addElOpen: false
    };

    updateActiveLocation = (pos, location) => {
        const newState = [...this.state.locations];
        newState.splice(pos, 1, location);
        this.setState({
            locations: newState
        });
    };

    openAddForm = () => {
        if (this.state.addElOpen === false) {
            this.setState({ addElOpen: true });
        } else {
            this.setState({ addElOpen: false });
        }
    };

    closeAddForm = () => {
        this.setState({ addElOpen: false });
    };

    getLocations = () => {
        LocationManager.getAllLocationsByAccount(
            this.props.accountId,
            this.state.search,
            this.state.category,
            this.state.tag,
            this.state.isActive
        ).then(data => {
            this.setState({ locations: data, loading: false });
            //console.log(data);
        });
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
        accountManager.getAccountById(this.props.accountId).then(data => {
            this.setState({
                accountId: data.accountId,
                city: data.city,
                accountName: data.name,
                cityLat: data.latitude,
                cityLng: data.longitude
            });
            console.log('account info', data);
        });

        this.getLocations();
    }

    render() {
        return (
            <>
                <section className='section-content'>
                    <div className='addViewRow'>
                        <Fab
                            color='primary'
                            aria-label='add'
                            onClick={() => this.openAddForm()}
                        >
                            <AddIcon />
                        </Fab>
                        {/* <FormDialog {...this.props} getData={this.getData} /> */}
                        <SwitchView changeView={this.changeView} />
                    </div>
                </section>
                {this.state.loading && (
                    <div className='spinner'>
                        <CircularProgress />
                    </div>
                )}

                {this.state.loading == false &&
                    this.state.locations.length == 0 && (
                        <DialogTitle className='modalTitle'>
                            {
                                'Sorry, you are not authorized to view this account'
                            }
                        </DialogTitle>
                    )}
                {this.state.accountId &&
                    this.state.loading == false &&
                    this.state.addElOpen && (
                        <AddLocationForm
                            accountId={this.state.accountId}
                            city={this.state.city}
                            cityLat={this.state.cityLat}
                            cityLng={this.state.cityLng}
                            getLocations={this.getLocations}
                            closeAddForm={this.closeAddForm}
                        />
                    )}

                {this.state.accountId && this.state.loading == false && (
                    <>
                        <DialogTitle className='modalTitle'>
                            {"Nashville's Locations"}
                        </DialogTitle>

                        {this.state.cardView ? (
                            <div className='container-cards'>
                                {this.state.locations.map(locationDetails => (
                                    <LocationCard
                                        key={locationDetails.locationId}
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
                                    getData={this.getLocations}
                                    {...this.props}
                                    updateActiveLocation={
                                        this.updateActiveLocation
                                    }
                                />
                            </div>
                        )}
                    </>
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
