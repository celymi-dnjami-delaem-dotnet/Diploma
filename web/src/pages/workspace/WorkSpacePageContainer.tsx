import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { ModalOptions, ModalTypes } from '../../constants/modal';
import { openModal } from '../../redux/actions/modal';
import { setSelectedProjectFromWorkSpaceById } from '../../redux/actions/project';
import { getUserWorkSpacePageRequest } from '../../redux/actions/workspace';
import { getWorkSpacePageProjects } from '../../redux/selectors/project';
import { getWorkSpace, getWorkSpaceIsLoading } from '../../redux/selectors/workspace';
import { IWorkSpace, IWorkSpacePageProject } from '../../types/workspace';
import WorkSpacePage, { IWorkSpacePageProps } from './WorkSpacePage';

const WorkSpacePageContainer = () => {
    const dispatch = useDispatch();
    const history = useHistory();

    const workSpace: IWorkSpace = useSelector(getWorkSpace);
    const isLoading: boolean = useSelector(getWorkSpaceIsLoading);
    const workSpaceProjects: IWorkSpacePageProject[] = useSelector(getWorkSpacePageProjects);

    const [selectedProjectId, setSelectedProjectId] = useState<string>('');

    const onChangeSelectedProjectId = (projectId: string): void => {
        setSelectedProjectId(projectId);
    };

    const onClickCreateProject = (): void => {
        dispatch(openModal(ModalTypes.PROJECT));
    };

    const onClickUpdateWorkSpaceInfo = (): void => {
        dispatch(openModal(ModalTypes.WORKSPACE, ModalOptions.WORKSPACE_UPDATE));
    };

    const onClickRemoveProject = (): void => {
        dispatch(setSelectedProjectFromWorkSpaceById(selectedProjectId));
        dispatch(openModal(ModalTypes.PROJECT, ModalOptions.PROJECT_REMOVE));
    };

    const onClickViewProject = (projectId: string): void => {
        history.push(`/project/${projectId}`);
    };

    const onClickViewTeam = (teamId: string): void => {
        history.push(`/team/${teamId}`);
    };

    useEffect(() => {
        dispatch(getUserWorkSpacePageRequest());
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const workSpacePageProps: IWorkSpacePageProps = {
        workSpace,
        isLoading,
        workSpaceProjects,
        selectedProjectId,
        onClickUpdateWorkSpaceInfo,
        onClickCreateProject,
        onClickViewProject,
        onClickViewTeam,
        onChangeSelectedProjectId,
        onClickRemoveProject,
    };

    return <WorkSpacePage {...workSpacePageProps} />;
};

export default WorkSpacePageContainer;
