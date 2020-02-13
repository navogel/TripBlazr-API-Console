import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { firstLetterCase } from '../../modules/helpers';
import LocationManager from '../../API/LocationManager';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
// import EditLocationModal from '../location/editlocationModal';

//table   {`https://localhost:5001${location.location.imageUrl}

class LocationCard extends Component {
    handleDelete = id => {
        LocationManager.deleteLocation(id).then(() => this.props.getData());
    };

    render() {
        console.log('locationCard props', this.props);
        return (
            <>
                <Card className='locationCard'>
                    <CardActionArea
                        className='cardActionArea'
                        onClick={() => {
                            this.props.toggleDrawer(this.props.locationDetails);
                        }}
                    >
                        {this.props.locationDetails.imageUrl ? (
                            <CardMedia
                                className='locationCardMedia'
                                image={`https://localhost:5001/upload/${
                                    this.props.locationDetails.imageUrl
                                }?${this.props.ranNum()}`}
                                alt='my dog'
                            />
                        ) : (
                            <CardMedia
                                className='locationCardMedia'
                                image={`https://localhost:5001/upload/logo.png`}
                                alt='my dog'
                            />
                        )}
                        <CardContent className='cardContent'>
                            <h3>
                                <span>
                                    {firstLetterCase(
                                        this.props.locationDetails.name
                                    )}
                                </span>
                            </h3>
                            <p>{this.props.locationDetails.description}</p>
                        </CardContent>
                    </CardActionArea>
                    <CardActions className='cardButtons'>
                        <Button
                            size='small'
                            color='primary'
                            onClick={() =>
                                this.handleDelete(
                                    this.props.locationDetails.locationId
                                )
                            }
                        >
                            Delete
                        </Button>

                        <Button
                            size='small'
                            color='primary'
                            onClick={() => {
                                this.props.toggleDrawer(
                                    this.props.locationDetails
                                );
                            }}
                        >
                            Details
                        </Button>
                    </CardActions>
                </Card>
            </>
        );
    }
}

export default LocationCard;
