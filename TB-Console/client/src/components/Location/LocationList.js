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
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import CheckBoxIcon from '@material-ui/icons/CheckBox';
import Checkbox from '@material-ui/core/Checkbox';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import LocationDrawer from './EditLocationDrawer';
import ToggleButton from '@material-ui/lab/ToggleButton';
import ToggleButtonGroup from '@material-ui/lab/ToggleButtonGroup';

const icon = <CheckBoxOutlineBlankIcon fontSize='small' />;
const checkedIcon = <CheckBoxIcon fontSize='small' />;

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
    addElOpen: false,
    itemsShown: 20,
    searchTarget: [],
    tagFilter: '',
    currentSearchCode: 1,
    alignment: 'center'
  };

  //spice for standard location array
  updateActiveLocation = (pos, location) => {
    //console.log('splice pos, location', pos location)
    const newState = [...this.state.locations];
    newState
      .filter(l => {
        if (this.state.searchTarget.length === 0) {
          return true;
        }
        return !!this.state.searchTarget.find(
          t => t.locationId === l.locationId
        );
      })
      .splice(pos, 1, location);
    this.setState({
      locations: newState
    });
  };
  //splice for search target array
  updateActiveLocation2 = (pos, location) => {
    const newState = [...this.state.searchTarget];
    newState.splice(pos, 1, location);
    this.setState({
      searchTarget: newState
    });
  };

  ranNum = () => {
    return Math.random() * (1000 - 1) + 1;
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

  onTagsChange = (event, values) => {
    this.setState({
      searchTarget: values
    });
  };

  UpdateCurrentLocations = () => {
    if (this.state.alignment === 'center') {
      this.getLocations();
    } else if (this.state.alignment === 'left') {
      this.getActiveLocations();
    } else {
      this.getInactiveLocations();
    }
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

  getActiveLocations = () => {
    LocationManager.getAllLocationsByAccount(
      this.props.accountId,
      this.state.search,
      this.state.category,
      this.state.tag,
      true
    ).then(data => {
      this.setState({ locations: data, loading: false });
      //console.log(data);
    });
  };

  getInactiveLocations = () => {
    LocationManager.getAllLocationsByAccount(
      this.props.accountId,
      this.state.search,
      this.state.category,
      this.state.tag,
      false
    ).then(data => {
      this.setState({ locations: data, loading: false });
      //console.log(data);
    });
  };

  toggleDrawer = obj => {
    // Access the handleToggle function of the drawer reference
    //onClick={this.toggleDrawer('right', true)
    this.refs.drawer.openDrawer(obj);
  };

  handleChange = (event, newAlignment) => {
    if (newAlignment !== null) {
      this.setState({
        alignment: newAlignment
      });
    }
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
    console.log('search target state', this.state.searchTarget);
    return (
      <>
        <section className='section-content'>
          <LocationDrawer
            ref='drawer'
            getLocations={this.UpdateCurrentLocations}
          />
          <div className='addViewRow'>
            <Fab
              color='primary'
              aria-label='add'
              onClick={() => this.openAddForm()}
            >
              <AddIcon />
            </Fab>

            <Autocomplete
              multiple
              id='checkboxes-tags-demo'
              options={this.state.locations}
              disableCloseOnSelect
              getOptionLabel={option =>
                typeof option === 'string' ? option : option.name
              }
              value={this.state.searchTarget}
              onChange={this.onTagsChange}
              renderOption={(option, { selected }) => (
                <React.Fragment>
                  <Checkbox
                    icon={icon}
                    checkedIcon={checkedIcon}
                    style={{ marginRight: 8 }}
                    checked={selected}
                  />
                  {option.name}
                </React.Fragment>
              )}
              style={{ width: 500 }}
              renderInput={params => (
                <TextField
                  {...params}
                  variant='outlined'
                  label='Selected Locations'
                  placeholder='Search'
                  fullWidth
                />
              )}
            />
            <SwitchView changeView={this.changeView} />
          </div>
        </section>
        {this.state.loading && (
          <div className='spinner'>
            <CircularProgress />
          </div>
        )}

        {this.state.loading === false && this.state.locations.length === 0 && (
          <DialogTitle className='modalTitle'>
            {'Sorry, you are not authorized to view this account'}
          </DialogTitle>
        )}
        {this.state.accountId &&
          this.state.loading === false &&
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

        {this.state.accountId && this.state.loading === false && (
          <>
            <div className='locationRow'>
              <DialogTitle className='modalTitle'>
                {"Nashville's Locations"}
              </DialogTitle>
              <ToggleButtonGroup
                value={this.state.alignment}
                size='small'
                exclusive
                onChange={this.handleChange}
                aria-label='text primary button group'
                exclusive
              >
                <ToggleButton
                  key={1}
                  onClick={e => this.getActiveLocations()}
                  value='left'
                >
                  Active
                </ToggleButton>

                <ToggleButton
                  className='middleToggle'
                  key={2}
                  onClick={e => this.getLocations()}
                  value='center'
                >
                  All
                </ToggleButton>
                <ToggleButton
                  key={3}
                  onClick={e => this.getInactiveLocations()}
                  value='right'
                >
                  Inactive
                </ToggleButton>
              </ToggleButtonGroup>
            </div>
            {this.state.cardView ? (
              <>
                <div className='container-cards'>
                  {this.state.locations
                    .filter(l => {
                      if (this.state.searchTarget.length === 0) {
                        return true;
                      }
                      return !!this.state.searchTarget.find(
                        t => t.locationId === l.locationId
                      );
                    })
                    .slice(0, this.state.itemsShown)
                    .map(locationDetails => (
                      <LocationCard
                        key={locationDetails.locationId}
                        locationDetails={locationDetails}
                        getData={this.getLocations}
                        {...this.props}
                        cardView={this.state.cardView}
                        toggleDrawer={this.toggleDrawer}
                        ranNum={this.ranNum}
                      />
                    ))}
                </div>

                <div className='loadMoreButton'>
                  {this.state.itemsShown && (
                    <Button
                      onClick={() => {
                        let newNum = this.state.itemsShown + 20;
                        this.setState({
                          itemsShown: newNum
                        });
                      }}
                      variant='contained'
                      color='primary'
                    >
                      Load More
                    </Button>
                  )}
                </div>
              </>
            ) : (
              <div className='container-table'>
                <LocationTable
                  locations={this.state.locations}
                  tagFilter={this.state.tagFilter}
                  getData={this.getLocations}
                  {...this.props}
                  updateActiveLocation={this.updateActiveLocation}
                  toggleDrawer={this.toggleDrawer}
                  ranNum={this.ranNum}
                  searchTarget={this.state.searchTarget}
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
