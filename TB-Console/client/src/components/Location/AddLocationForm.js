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
class AddLocationForm extends Component {
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
        inputImageValue: '',

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

    handleImageChange(event) {
        this.setState({ inputImageValue: event.target.files[0] });
    }
    /*  Local method for validation, set loadingStatus, create animal object, invoke the AnimalManager post method, and redirect to the full animal list
     */
    constructNewLocation = evt => {
        evt.preventDefault();
        if (
            this.state.name === '' ||
            this.state.latitude === '' ||
            this.state.longitude === ''
        ) {
            window.alert(
                'Well this is awkward...  you have to enter a name and address to get location coordinated.'
            );
        } else {
            this.setState({ loadingStatus: true });

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
            formData.append('file', this.state.inputImageValue);

            LocationManager.postLocation(formData).then(() => {
                this.props.getData();
                this.setState({ loadingStatus: false });
            });

            //location model for submission without image

            // const location = {
            //     accountId: this.state.accountId,
            //     name: this.state.name,
            //     phoneNumber: this.state.phoneNumber,
            //     website: this.state.website,
            //     shortSummary: this.state.shortSummary,
            //     description: this.state.description,
            //     latitude: this.state.latitude,
            //     longitude: this.state.longitude,
            //     sortId: this.state.sortId,
            //     videoId: this.state.videoId,
            //     videoStartTime: this.state.videoStartTime,
            //     videoEndTime: this.state.videoEndTime,
            //     address1: this.state.address1,
            //     address2: this.state.address2,
            //     city: this.state.city,
            //     zipcode: this.state.zipcode,
            //     state: this.state.state,
            //     isActive: this.state.isActive,
            //     file: this.state.image
            // };
        }
    };
    componentDidMount() {
        this.setState({
            labelWidth: ReactDOM.findDOMNode(this.InputLabelRef).offsetWidth
        });
        console.log('add location props', this.props);
        //add some logic here to manage the obj that is passed

        this.setState({
            accountId: this.props.accountId
        });
    }

    render() {
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
                                {'OK, lets start with the name and address:'}
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
                                        Primary Type
                                    </InputLabel>
                                    <NativeSelect
                                        value={this.state.locationTypeId}
                                        onChange={this.handleChange(
                                            'locationTypeId'
                                        )}
                                        input={
                                            <OutlinedInput
                                                name='Primary Category'
                                                labelWidth={
                                                    this.state.labelWidth
                                                }
                                                id='primaryCategory'
                                            />
                                        }
                                    >
                                        <option value='' />
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

export default withStyles(styles)(AddLocationForm);
