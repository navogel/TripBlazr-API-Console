import React, { Component } from 'react';
import AccountManager from '../API/accountManager';
import AccountMapper from './map/AccountMapper';
import 'leaflet/dist/leaflet.css';
class Home extends Component {
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
        AccountManager.getAllAccounts().then(data => {
            this.setState({ accounts: data });
            console.log('home account data', data);
        });
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
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
                <ul>
                    {this.state.accounts.map(account => (
                        <div key={account.accountId}>
                            <div>{account.city}</div>
                            <button>Locations</button>

                            {/* <Link
                                to={`/accounts/${this.account.accountId}/locations`}
                            >
                                <button>Locations</button>
                            </Link> */}
                            <div className={'mapWrapper'}>
                                <AccountMapper
                                    className={'map'}
                                    latitude={account.latitude}
                                    longitude={account.longitude}
                                    address={this.state.address}
                                />
                            </div>
                        </div>
                    ))}
                </ul>
            </>
        );
    }
}

export default Home;
