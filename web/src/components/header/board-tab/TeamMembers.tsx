import { Avatar, Tooltip } from '@material-ui/core';
import { createStyles, makeStyles } from '@material-ui/core/styles';
import { Add } from '@material-ui/icons';
import AvatarGroup from '@material-ui/lab/AvatarGroup';
import React from 'react';
import { UserPosition, UserRole } from '../../../constants/userConstants';
import { ITeam } from '../../../types/teamTypes';
import { getFirstNameLetter, isUserCustomer, isUserProjectManager } from '../../../utils';

const useStyles = makeStyles(() =>
    createStyles({
        addUserButton: {
            borderRadius: '50%',
            width: '43px',
            height: '43px',
            backgroundColor: '#75BAF7',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            color: '#FFF',
            cursor: 'pointer',
            marginRight: '10px',
        },
    })
);

export interface ITeamMembersProps {
    team: ITeam;
    userId: string;
    userRole: UserRole;
    userPosition: UserPosition;
    onClickCreateUser: () => void;
}

const TeamMembers = (props: ITeamMembersProps) => {
    const classes = useStyles();
    const { userId, userRole, userPosition, team, onClickCreateUser } = props;

    return (
        <>
            {(isUserCustomer(userRole, userPosition) || isUserProjectManager(userRole, userPosition)) && (
                <Tooltip title="Add new team member">
                    <div className={classes.addUserButton} onClick={onClickCreateUser}>
                        <Add />
                    </div>
                </Tooltip>
            )}
            <AvatarGroup max={5} spacing="small">
                {team &&
                    team.users &&
                    team.users.map((x, index) => {
                        return x.userId !== userId ? (
                            <Tooltip title={x.userName} key={x.userId}>
                                <Avatar src={x.avatarLink}>{getFirstNameLetter(x.userName)}</Avatar>
                            </Tooltip>
                        ) : null;
                    })}
            </AvatarGroup>
        </>
    );
};

export default TeamMembers;
