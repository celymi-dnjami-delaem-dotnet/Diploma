import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { BaseRegexExpression } from '../../../constants';
import { ModalOptions } from '../../../constants/modal';
import { InitialWorkSpaceFormValues } from '../../../constants/workspace';
import { createWorkSpaceRequest, updateWorkSpaceRequest } from '../../../redux/actions/workspace';
import { getModalOption, getModalRequestPerforming } from '../../../redux/selectors/modal';
import { getWorkSpace } from '../../../redux/selectors/workspace';
import { IWorkSpaceForm } from '../../../types/forms';
import { IWorkSpace } from '../../../types/workspace';
import { validateInputFormField } from '../../../utils/forms';
import WorkSpaceModal, { IWorkSpaceModalProps } from './WorkSpaceModal';

const WorkSpaceModalContainer = () => {
    const dispatch = useDispatch();
    const modalOption: ModalOptions = useSelector(getModalOption);
    const workSpaceDescription: IWorkSpace = useSelector(getWorkSpace);
    const isPerformingRequest: boolean = useSelector(getModalRequestPerforming);

    const isUpdate: boolean = modalOption === ModalOptions.WORKSPACE_UPDATE;
    const initialState: IWorkSpaceForm = isUpdate
        ? {
              ...workSpaceDescription,
          }
        : InitialWorkSpaceFormValues;

    const onSubmitButton = (values: IWorkSpaceForm): void => {
        const workSpace: IWorkSpace = {
            workSpaceId: values.workSpaceId,
            workSpaceName: values.workSpaceName,
            workSpaceDescription: values.workSpaceDescription,
            creationDate: values.creationDate,
        };

        if (isUpdate) {
            dispatch(updateWorkSpaceRequest(workSpace));
        } else {
            dispatch(createWorkSpaceRequest(workSpace));
        }
    };

    const validateWorkSpaceName = (value: string) => {
        const isRequired = true;
        const minLength = 3;
        const maxLength = 100;

        return validateInputFormField(value, isRequired, minLength, maxLength, BaseRegexExpression);
    };

    const workSpaceModalProps: IWorkSpaceModalProps = {
        isPerformingRequest,
        isUpdate,
        initialState,
        onSubmitButton,
        validateWorkSpaceName,
    };

    return <WorkSpaceModal {...workSpaceModalProps} />;
};

export default WorkSpaceModalContainer;
