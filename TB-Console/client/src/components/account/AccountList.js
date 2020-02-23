import React, { Component } from 'react';
import AccountManager from '../../API/accountManager';
import AccountMapper from '../map/AccountMapper';
import { Link } from 'react-router-dom';

class AccountList extends Component {
    state = {
        values: [],
        accounts: [],
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
        console.log('im account list page', this.props);
        //creat auth header for every request

        AccountManager.getAllAccounts().then(data => {
            this.setState({ accounts: data });
            console.log('im the account data', data);
        });
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
                {this.state.accounts && (
                    <>
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
                            {this.state.accounts.map(account => (
                                <div key={account.accountId}>
                                    <li>{account.city}</li>
                                    <div className={'mapWrapper'}>
                                        <AccountMapper
                                            className={'map'}
                                            latitude={account.latitude}
                                            longitude={account.longitude}
                                            address={this.state.address}
                                        />
                                    </div>
                                    <Link
                                        to={`/accounts/${account.accountId}/locations`}
                                    >
                                        <button>Locations</button>
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
