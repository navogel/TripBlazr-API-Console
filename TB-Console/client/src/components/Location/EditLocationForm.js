import React, { Component } from 'react';
import LocationManager from '../../API/LocationManager';
import DialogTitle from '@material-ui/core/DialogTitle';
import ReactDOM from 'react-dom';
//import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import FormControl from '@material-ui/core/FormControl';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputLabel from '@material-ui/core/InputLabel';
import NativeSelect from '@material-ui/core/NativeSelect';
import InputAdornment from '@material-ui/core/InputAdornment';
import Button from '@material-ui/core/Button';
import LocationDetailsMapper from '../map/LocationDetailsMapper';

const styles = theme => ({
    container: {
        display: 'flex',
        flexWrap: 'wrap'
    },
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1)
    },
    dense: {
        marginTop: 16
    },
    menu: {
        width: 400
    },
    extendedIcon: {
        marginRight: theme.spacing(1)
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 200
    }
});
class EditLocationForm extends Component {
    state = {
        accountId: '',
        name: '',
        phoneNumber: '',
        website: '',
        shortSummary: '',
        description: '',
        latitude: '',
        longitude: '',
        sortId: '',
        videoId: '',
        videoStartTime: '',
        videoEndTime: '',
        address1: '',
        address2: '',
        city: '',
        zipcode: '',
        state: '',
        isActive: '',
        //file: '',

        //mapping
        mapAddress: '',

        //category
        primaryCategory: '',

        //page state
        loadingStatus: false,
        labelWidth: 0
        // imageLink: ''
    };

    handleFieldChange = evt => {
        const stateToChange = {};
        stateToChange[evt.target.id] = evt.target.value;
        this.setState(stateToChange);
    };

    handleChange = name => event => {
        this.setState({ [name]: event.target.value });
    };

    //pass to the map

    submitAddress = () => {
        //console.log(value);
        this.setState({
            mapAddress: `${this.state.name} ${this.state.address1} ${this.state.city}`
        });
    };

    grabCoordsFromPin = (lat, lng) => {
        this.setState({
            latitude: lat,
            longitude: lng
        });
    };

    constructNewLocation = evt => {
        evt.preventDefault();
        if (
            this.state.name === '' ||
            this.state.latitude === '' ||
            this.state.longitude === '' ||
            this.state.primaryCategory === ''
        ) {
            window.alert(
                'Well this is awkward...  you need to fill out these items to create a category!.'
            );
        } else {
            this.setState({ loadingStatus: true });
            const fileInput = document.querySelector('#fileInput');
            console.log('image data', fileInput);
            let formData = new FormData();

            formData.append('accountId', this.state.accountId);
            formData.append('name', this.state.name);
            formData.append('phoneNumber', this.state.phoneNumber);
            formData.append('website', this.state.website);
            formData.append('shortSummary', this.state.shortSummary);
            formData.append('description', this.state.description);
            formData.append('latitude', this.state.latitude);
            formData.append('longitude', this.state.longitude);
            formData.append('sortId', this.state.sortId);
            formData.append('videoId', this.state.videoId);
            formData.append('address1', this.state.address1);
            formData.append('city', this.state.city);
            formData.append('zipcode', this.state.zipcode);
            formData.append('state', this.state.state);
            formData.append('isActive', this.state.isActive);
            formData.append('file', fileInput.files[0]);

            LocationManager.createLocation(formData).then(data => {
                // console.log(
                //     'return from post',
                //     data.locationId,
                //     this.state.primaryCategory
                // );
                LocationManager.createPrimaryCategory(
                    data.locationId,
                    parseInt(this.state.primaryCategory)
                );
                this.props.getLocations();
                this.setState({
                    loadingStatus: false,

                    name: '',
                    phoneNumber: '',
                    website: '',
                    shortSummary: '',
                    description: '',
                    latitude: '',
                    longitude: '',
                    sortId: '',
                    videoId: '',
                    videoStartTime: '',
                    videoEndTime: '',
                    address1: '',
                    address2: '',
                    city: '',
                    zipcode: '',
                    state: '',
                    isActive: '',
                    primaryCategory: ''
                });
                fileInput.value = '';
            });
        }
    };
    componentDidMount() {
        console.log('props to edit form', this.props);
        // this.setState({
        //     labelWidth: ReactDOM.findDOMNode(this.InputLabelRef).offsetWidth
        // });
        LocationManager.getLocationById(this.props.locationId).then(data => {
            console.log('grab location by id', data);
            this.setState({
                accountId: this.props.accountId,
                labelWidth: ReactDOM.findDOMNode(this.InputLabelRef)
                    .offsetWidth,
                name: data.name,
                phoneNumber: data.phonenumber,
                website: data.website,
                shortSummary: data.shortSummary,
                description: data.description,
                latitude: data.latitude,
                longitude: data.longitude,
                sortId: data.sortId,
                videoId: data.videoId,
                videoStartTime: data.videoStartTime,
                videoEndTime: data.videoEndTime,
                address1: data.address1,
                address2: data.address2,
                city: data.city,
                zipcode: data.zipcode,
                state: data.state,
                isActive: data.isActive,
                primaryCategory: data.categories.find(c => c.isPrimary == true)
            });
            console.log('primary cat', this.state.primaryCategory);
        });

        //add some logic here to manage the obj that is passed

        // this.setState({
        //     accountId: this.props.accountId
        // });
    }

    render() {
        console.log('latlng state', this.state.latitude, this.state.longitude);
        const { classes } = this.props;

        return (
            <>
                <div className='formContainer'>
                    <form
                        className={classes.container}
                        noValidate
                        autoComplete='off'
                    >
                        <div className='infoWrapper'>
                            <DialogTitle className='modalTitle'>
                                {'OK, here is everything you can edit:'}
                            </DialogTitle>
                            <div className='locationInputWrapper'>
                                <TextField
                                    id='name'
                                    label='Name'
                                    className={classes.textField}
                                    value={this.state.name}
                                    onChange={this.handleFieldChange}
                                    margin='dense'
                                    variant='outlined'
                                    placeholder='Enter the place name'
                                />

                                <FormControl
                                    variant='outlined'
                                    margin='dense'
                                    className={classes.formControl}
                                >
                                    <InputLabel
                                        ref={ref => {
                                            this.InputLabelRef = ref;
                                        }}
                                        htmlFor='outlined-type-native-simple'
                                    >
                                        Primary Category
                                    </InputLabel>
                                    <NativeSelect
                                        value={this.state.primaryCategory}
                                        onChange={this.handleChange(
                                            'primaryCategory'
                                        )}
                                        input={
                                            <OutlinedInput
                                                name='primaryCategory'
                                                labelWidth={
                                                    this.state.labelWidth
                                                }
                                                id='primaryCategory'
                                            />
                                        }
                                    >
                                        <option value={1}>Stay</option>
                                        <option value={2}>Eat</option>
                                        <option value={3}>Drink</option>
                                        <option value={4}>Hear</option>
                                        <option value={5}>Play</option>
                                        <option value={6}>See</option>
                                        <option value={7}>Shop</option>
                                    </NativeSelect>
                                </FormControl>

                                {/* <div className='midFormText'>
                                    <p> Optional:</p>
                                </div> */}

                                <TextField
                                    id='address1'
                                    label='Address'
                                    className={classes.textField}
                                    value={this.state.address1}
                                    onChange={this.handleFieldChange}
                                    margin='dense'
                                    variant='outlined'
                                    placeholder='Enter Address'
                                />
                                <TextField
                                    id='city'
                                    label='City'
                                    className={classes.textField}
                                    value={this.state.city}
                                    onChange={this.handleFieldChange}
                                    margin='dense'
                                    variant='outlined'
                                    placeholder='Enter City'
                                />
                            </div>
                            <Button
                                variant='contained'
                                // size='small'
                                color='primary'
                                //disabled={this.state.loadingStatus}
                                onClick={this.submitAddress}
                            >
                                Get Pin From Address
                            </Button>
                            <LocationDetailsMapper
                                cityLat={this.state.latitude}
                                cityLng={this.state.longitude}
                                mapAddress={this.state.mapAddress}
                                grabCoordsFromPin={this.grabCoordsFromPin}

                                // address={this.state.address1}
                                // name={this.state.name}
                                // city={this.state.city}
                            />

                            <input
                                type='file'
                                name='file'
                                id='fileInput'
                                accept='.png, .jpg'
                                onChange={this.fileSelectHandler}
                            />
                            <br />

                            <div className='formSubmit'>
                                <Button
                                    variant='contained'
                                    // size='small'
                                    color='primary'
                                    aria-label='submit'
                                    className={classes.margin}
                                    disabled={this.state.loadingStatus}
                                    onClick={this.constructNewLocation}
                                >
                                    {/* <AddIcon className={classes.extendedIcon} /> */}
                                    Submit
                                </Button>
                            </div>
                        </div>
                    </form>
                </div>
            </>
        );
    }
}

export default withStyles(styles)(EditLocationForm);
