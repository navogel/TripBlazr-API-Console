import { Route, Redirect } from 'react-router-dom';
import React, { Component } from 'react';
import Home from './Home';
import LocationList from './location/LocationList';
import EditLocationForm from './location/EditLocationForm';
import Login from '../components/Login';
import AccountDetails from './account/AccountDetails';
import AccountList from './account/AccountList';

class ApplicationViews extends Component {
  //check for login before showing content

  render() {
    //console.log(this.props.user);
    return (
      <React.Fragment>
        {/* <Route
                    exact
                    path='/'
                    render={props => {
                        return <Home />;
                    }}
                /> */}

        <Route
          exact
          path='/'
          render={props => {
            if (this.props.user) {
              return <AccountList user={this.props.user} {...props} />;
            } else {
              return <Redirect to='/login' />;
            }
          }}
        />
        <Route
          exact
          path='/accounts/:accountId(\d+)'
          render={props => {
            if (this.props.user) {
              return (
                <AccountDetails
                  AccountId={parseInt(props.match.params.accountId)}
                  {...props}
                />
              );
            } else {
              return <Redirect to='/login' />;
            }
          }}
        />
        <Route
          exact
          path='/accounts/:accountId(\d+)/Locations'
          render={props => {
            if (this.props.user) {
              return (
                <LocationList
                  accountId={parseInt(props.match.params.accountId)}
                  {...props}
                />
              );
            } else {
              return <Redirect to='/login' />;
            }
          }}
        />
        {/* <Route
          exact
          path='/accounts/:accountId(\d+)/Locations/:locationId(\d+)'
          render={props => {
            if (this.props.user) {
              return (
                <EditLocationForm
                  accountId={parseInt(props.match.params.accountId)}
                  locationId={parseInt(props.match.params.locationId)}
                  {...props}
                />
              );
            } else {
              return <Redirect to='/login' />;
            }
          }}
        /> */}

        {/* <Route
                    exact
                    path='/login'
                    render={props => {
                        return (
                            <Login setUser={this.props.setUser} {...props} />
                        );
                    }}
                /> */}
        {/* <Route render={() => <h1>404 Error</h1>} /> */}
      </React.Fragment>
    );
  }
}

export default ApplicationViews;
