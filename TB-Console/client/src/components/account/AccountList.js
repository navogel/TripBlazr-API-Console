import React, { Component } from 'react';
import { createAuthHeaders } from '../../API/userManager';
import AccountManager from '../../API/accountManager';
import AccountMapper from '../map/AccountMapper';
import { Link } from 'react-router-dom';
import { Route, Redirect } from 'react-router-dom';
import Button from '@material-ui/core/Button';

class AccountList extends Component {
  state = {
    values: [],
    accounts: [],
    address: '',
    tempAddress: '',
    toLogin: false
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
    console.log('im account list page', this.props);
    //creat auth header for every request

    AccountManager.getAllAccounts().then(data => {
      if (data.response) {
        this.setState({ toLogin: true });
      } else {
        this.setState({ accounts: data });
        console.log('im the account data', data);
      }
    });
  }

  render() {
    if (this.state.toLogin === true) {
      return <Redirect to='/login' />;
    }
    return (
      <>
        <h1>Welcome to TripBlazr Console</h1>
        {this.state.accounts && (
          <>
            <div>
              {this.state.accounts.map(account => (
                <div key={account.accountId}>
                  <div className={'mapWrapper'}>
                    <AccountMapper
                      className={'map'}
                      latitude={account.latitude}
                      longitude={account.longitude}
                      address={this.state.address}
                    />
                  </div>
                  <Link to={`/accounts/${account.accountId}/locations`}>
                    <Button variant='contained' color='primary'>
                      Manage Locations
                    </Button>
                  </Link>
                </div>
              ))}
            </div>
          </>
        )}
      </>
    );
  }
}

export default AccountList;
