import * as epicActions from '../actions/epicActions';
import { IEpicsState } from '../store/state';

const initialState: IEpicsState = {
    epics: [],
    currentEpic: null,
};

export default function epicReducer(state = initialState, action: epicActions.EpicActionTypes) {
    switch (action.type) {
        case epicActions.EpicActions.CREATE_EPIC_SUCCESS:
            return handleCreateEpicSuccess(state, action);
        case epicActions.EpicActions.GET_EPICS_SUCCESS:
            return handleGetEpicsSuccess(state, action);
        case epicActions.EpicActions.SET_CURRENT_EPIC:
            return handleSetCurrentEpic(state, action);
        default:
            return state;
    }
}

function handleCreateEpicSuccess(state: IEpicsState, action: epicActions.ICreateEpicSuccess): IEpicsState {
    return {
        ...state,
        epics: [...state.epics, action.payload],
    };
}

function handleGetEpicsSuccess(state: IEpicsState, action: epicActions.IGetEpicsSuccess): IEpicsState {
    return {
        ...state,
        epics: action.payload,
    };
}

function handleSetCurrentEpic(state: IEpicsState, action: epicActions.ISetCurrentEpic): IEpicsState {
    return {
        ...state,
        currentEpic: action.payload,
    };
}
