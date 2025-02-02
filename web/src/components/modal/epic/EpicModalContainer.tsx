import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { EpicInitialState } from '../../../constants/epicConstants';
import { ModalOptions } from '../../../constants/modalConstants';
import { removeEpicRequest } from '../../../redux/actions/epicActions';
import * as epicActions from '../../../redux/actions/epicActions';
import { getSelectedEpic } from '../../../redux/selectors/epicSelectors';
import { getModalOption, getModalRequestPerforming } from '../../../redux/selectors/modalSelectors';
import { getSelectedProject } from '../../../redux/selectors/projectSelectors';
import { IEpic } from '../../../types/epicTypes';
import { IEpicFormTypes } from '../../../types/formTypes';
import { IProject } from '../../../types/projectTypes';
import { InputFormFieldValidator } from '../../../utils/formUtils';
import ModalRemove, { IModalRemoveProps } from '../ModalRemove';
import EpicModal, { IEpicCreationProps } from './EpicModal';

const EpicModalContainer = () => {
    const dispatch = useDispatch();
    const project: IProject = useSelector(getSelectedProject);
    const modalOption: ModalOptions = useSelector(getModalOption);
    const selectedEpic: IEpic = useSelector(getSelectedEpic);
    const isPerformingRequest: boolean = useSelector(getModalRequestPerforming);

    const isUpdate: boolean = modalOption === ModalOptions.EPIC_UPDATE;
    const isRemove: boolean = modalOption === ModalOptions.EPIC_REMOVE;
    const initialValues: IEpicFormTypes = isUpdate
        ? {
              ...selectedEpic,
              epicId: selectedEpic.epicId,
          }
        : EpicInitialState;

    const onSubmitButton = (values: IEpicFormTypes): void => {
        const epic: IEpic = {
            ...values,
            epicId: values.epicId,
            projectId: project.projectId,
            creationDate: isUpdate ? selectedEpic.creationDate : null,
        };

        if (isUpdate) {
            dispatch(epicActions.updateEpicRequest(epic));
        } else {
            dispatch(epicActions.createEpicRequest(epic));
        }
    };

    const onClickRemoveEpic = (): void => {
        dispatch(removeEpicRequest(selectedEpic.epicId));
    };

    const validateEpicName = (value: string) => new InputFormFieldValidator(value, 3, 100, true, null).validate();

    const epicRemoveProps: IModalRemoveProps = {
        entity: 'epic',
        entityName: selectedEpic ? selectedEpic.epicName : '',
        onClick: onClickRemoveEpic,
    };

    const epicCreationProps: IEpicCreationProps = {
        isPerformingRequest,
        isUpdate,
        initialValues,
        validateEpicName,
        onSubmitButton,
    };

    return isRemove ? <ModalRemove {...epicRemoveProps} /> : <EpicModal {...epicCreationProps} />;
};

export default EpicModalContainer;
