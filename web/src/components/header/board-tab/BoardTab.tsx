import { createStyles, makeStyles } from '@material-ui/core/styles';
import AddIcon from '@material-ui/icons/Add';
import React from 'react';
import { ISelectedItem } from '../../../types/storyTypes';
import { ITeam } from '../../../types/teamTypes';
import Button from '../../common/Button';
import BoardTabDropdown from './BoardTabDropdown';
import ProjectDisplay from './ProjectDisplay';
import TeamMembers from './TeamMembers';

const useStyles = makeStyles(() =>
    createStyles({
        root: {
            minWidth: '100%',
            height: '70px',
        },
        tabContainer: {
            padding: '0 30px',
            height: 'inherit',
            display: 'flex',
            flexDirection: 'row',
        },
        buttonsContainer: {
            display: 'inherit',
            justifyContent: 'center',
            alignItems: 'center',
            margin: '0 0 0 auto',
        },
        selectContainer: {
            marginLeft: '30px',
        },
        selectStyle: {
            height: '45px',
            width: '140px',
        },
        selectTitle: {
            fontSize: '16px',
            fontFamily: 'Poppins, sans-serif',
        },
        epicsContainer: {
            marginRight: '30px',
        },
        buttonContainer: {
            width: '140px',
            marginLeft: '20px',
        },
        text: {
            fontSize: '20px',
            fontFamily: 'Poppins',
            fontWeight: 500,
            color: '#242126',
        },
        projectLabel: {
            fontSize: '16px',
            color: '#AFC1C4',
        },
    })
);

export interface IBoardTabProps {
    userId: string;
    team: ITeam;
    projectName: string;
    sortFields: ISelectedItem[];
    selectedEpicId: string;
    epics: ISelectedItem[];
    sortType: string;
    onChangeEpic: (value: string) => void;
    onChangeSortType: (e) => void;
    onClickAddStory: () => void;
    onClickCreateUser: () => void;
}

const BoardTab = (props: IBoardTabProps) => {
    const classes = useStyles();
    const {
        projectName,
        team,
        userId,
        selectedEpicId,
        epics,
        sortFields,
        sortType,
        onChangeSortType,
        onClickAddStory,
        onChangeEpic,
        onClickCreateUser,
    } = props;

    return (
        <div className={classes.root}>
            <div className={classes.tabContainer}>
                <ProjectDisplay projectName={projectName} />
                <div className={classes.buttonsContainer}>
                    {selectedEpicId && (
                        <div className={classes.epicsContainer}>
                            <BoardTabDropdown value={selectedEpicId} items={epics} onChangeEvent={onChangeEpic} />
                        </div>
                    )}
                    <TeamMembers team={team} userId={userId} onClickCreateUser={onClickCreateUser} />
                    <div className={classes.selectContainer}>
                        <BoardTabDropdown value={sortType} items={sortFields} onChangeEvent={onChangeSortType} />
                    </div>
                    <div className={classes.buttonContainer}>
                        <Button startIcon={<AddIcon />} onClick={onClickAddStory} label="Add task" disabled={false} />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default BoardTab;
