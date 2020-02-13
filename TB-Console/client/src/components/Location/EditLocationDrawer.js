import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import EditLocationForm from './EditLocationForm';

const styles = {
    list: {
        width: 1000
    },
    fullList: {
        width: 'auto'
    }
};

class LocationDrawer extends React.Component {
    state = {
        right: false,
        location: {}
    };

    toggleDrawer = (side, open) => () => {
        this.setState({
            [side]: open
        });
    };

    openDrawer = obj => {
        this.setState({
            right: true,
            location: obj
        });
    };

    closeDrawer = () => {
        this.setState({
            right: false,
            location: {}
        });
    };

    render() {
        return (
            <>
                <Drawer
                    anchor='right'
                    open={this.state.right}
                    onClose={this.toggleDrawer('right', false)}
                >
                    <div tabIndex={0} role='button'>
                        {/* {sideList} */}
                        <div className='drawerWrapper'>
                            <EditLocationForm
                                location={this.state.location}
                                getLocations={this.props.getLocations}
                                closeDrawer={this.closeDrawer}
                            />
                        </div>
                    </div>
                </Drawer>
                {/* </ClickAwayListener> */}
            </>
        );
    }
}

LocationDrawer.propTypes = {
    classes: PropTypes.object.isRequired
};

export default withStyles(styles)(LocationDrawer);
