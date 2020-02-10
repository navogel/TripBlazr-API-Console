import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import DeleteIcon from '@material-ui/icons/DeleteOutlined';
import LocationManager from '../../API/LocationManager';
import AssignmentIcon from '@material-ui/icons/Assignment';
import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';
import Switch from '@material-ui/core/Switch';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import EditIcon from '@material-ui/icons/EditOutlined';
import Checkbox from '@material-ui/core/Checkbox';

// import EditLocationModalTable from '../location/editlocationModalTable';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        marginTop: theme.spacing(2),
        overflowX: 'auto'
    },
    table: {
        minWidth: 650
    },
    large: {
        width: theme.spacing(7),
        height: theme.spacing(7)
    }
}));

export default function LocationTable(props) {
    const classes = useStyles();
    const handleDelete = id => {
        LocationManager.delete(id).then(() => props.getData());
    };
    return (
        <div className={'tableWrapper'}>
            <Paper className={classes.root}>
                <Table stickyHeader className={classes.table}>
                    <TableHead>
                        <TableRow>
                            <TableCell align='right'></TableCell>
                            <TableCell align='left'>Name</TableCell>
                            <TableCell align='left'>Details</TableCell>
                            <TableCell align='right' className='activeText'>
                                Active
                            </TableCell>
                            <TableCell className='editDeleteIcons'>
                                Edit - Details - Delete{' '}
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {props.locations.map(location => (
                            <TableRow key={location.name}>
                                <TableCell
                                    component='th'
                                    scope='row'
                                    className='imageCell'
                                >
                                    <div className='tableIcons'>
                                        {location.imageUrl ? (
                                            <Avatar
                                                variant='rounded'
                                                alt='location image'
                                                src={`https://localhost:5001/upload/${location.imageUrl}`}
                                                className={classes.large}
                                            />
                                        ) : (
                                            <Avatar
                                                variant='rounded'
                                                alt='location image'
                                                src={`https://localhost:5001/upload/logo.png`}
                                                className={classes.large}
                                            />
                                        )}
                                    </div>
                                </TableCell>
                                <TableCell
                                    align='left'
                                    className='locationNameCell'
                                >
                                    {location.name}
                                </TableCell>
                                <TableCell align='left'>
                                    {location.description}
                                </TableCell>
                                <TableCell align='right' className='switchCell'>
                                    <FormGroup row>
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    checked={location.isActive}
                                                    // onChange={handleChange(
                                                    //     'checkedB'
                                                    // )}
                                                    value='isActive'
                                                    color='primary'
                                                />
                                                // <Switch
                                                //     checked={!location.isActive}
                                                //     // onChange={handleChange(
                                                //     //     'checkedB'
                                                //     // )}
                                                //     value={false}
                                                //     color='primary'
                                                // />
                                            }
                                            // label='Toggle View'
                                        />
                                    </FormGroup>
                                </TableCell>
                                <TableCell align='right' className='iconCell'>
                                    <IconButton
                                        aria-label='delete'
                                        onClick={() =>
                                            handleDelete(location.id)
                                        }
                                    >
                                        <EditIcon />
                                    </IconButton>
                                    <IconButton
                                        aria-label='details'
                                        onClick={() => {
                                            props.history.push(
                                                `/locations/${location.id}`
                                            );
                                        }}
                                    >
                                        <AssignmentIcon />
                                    </IconButton>
                                    <IconButton
                                        aria-label='delete'
                                        //onClick={() => handleDelete(animal.id)}
                                    >
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>
        </div>
    );
}
