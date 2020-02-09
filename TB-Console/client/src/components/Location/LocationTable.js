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
// import EditLocationModalTable from '../location/editlocationModalTable';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        marginTop: theme.spacing(3),
        overflowX: 'auto'
    },
    table: {
        minWidth: 650
    }
}));

export default function LocationTable(props) {
    const classes = useStyles();
    const handleDelete = id => {
        LocationManager.delete(id).then(() => props.getData());
    };
    return (
        <Paper className={classes.root}>
            <Table className={classes.table}>
                <TableHead>
                    <TableRow>
                        <TableCell className='editDeleteIcons'>
                            Edit - Delete - Details{' '}
                        </TableCell>
                        <TableCell align='right'>ID</TableCell>
                        <TableCell align='right'>Name</TableCell>
                        <TableCell align='right'>Breed</TableCell>
                        <TableCell align='right'>Employee</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {props.locations.map(location => (
                        <TableRow key={location.name}>
                            <TableCell component='th' scope='row'>
                                <div className='tableIcons'>
                                    {/* <EditLocationModalTable id={location.id} props={props} /> */}
                                    <IconButton
                                        aria-label='delete'
                                        onClick={() =>
                                            handleDelete(location.id)
                                        }
                                    >
                                        <DeleteIcon />
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
                                </div>
                            </TableCell>
                            <TableCell align='right'>{location.id}</TableCell>
                            <TableCell align='right'>{location.name}</TableCell>
                            <TableCell align='right'>
                                {location.breed}
                            </TableCell>
                            <TableCell align='right'>
                                {location.employee.name}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Paper>
    );
}
