import * as UserActions from '../actions/userActions';
import { IUserState } from '../store/state';

const initialState: IUserState = {
    isAuthenticationSuccessful: false,
    wasCustomerCreated: false,
    user: null,
    isLoading: false,
    selectedTeam: '',
    selectedProject: '',
    emailExists: false,
};

export default function userReducer(state = initialState, action) {
    switch (action.type) {
        case UserActions.UserActions.VERIFY_USER_REQUEST:
        case UserActions.UserActions.AUTHENTICATION_REQUEST:
            return handleVerifyUserRequest(state);
        case UserActions.UserActions.ADD_USER:
        case UserActions.UserActions.VERIFY_USER_SUCCESS:
        case UserActions.UserActions.UPDATE_PROFILE_SETTINGS_SUCCESS:
            return handleGetUser(state, action);
        case UserActions.UserActions.VERIFY_USER_FAILURE:
            return handleVerifyUserFailure(state);
        case UserActions.UserActions.AUTHENTICATION_SUCCESS:
            return handleAuthenticationSuccess(state, action);
        case UserActions.UserActions.AUTHENTICATION_FAILURE:
            return handleAuthenticationFailure(state);
        case UserActions.UserActions.REGISTRATION_SUCCESS:
            return handleRegistrationSuccess(state);
        case UserActions.UserActions.HIDE_CUSTOMER_SUCCESSFUL_REGISTRATION:
            return handleHideCustomerSuccessfulRegistration(state);
        case UserActions.UserActions.UPDATE_AVATAR_SUCCESS:
            return handleUpdateAvatarLink(state, action);
        case UserActions.UserActions.CHANGE_USER_PROJECT:
            return handleChangeUserProject(state, action);
        case UserActions.UserActions.CHANGE_USER_TEAM:
            return handleChangeUserTeam(state, action);
        case UserActions.UserActions.LOGOUT_USER:
            return handleUserLogOut();
        case UserActions.UserActions.CHECK_EMAIL_EXISTENCE_SUCCESS:
            return handleEmailExistence(state, action);
        case UserActions.UserActions.RESET_EMAIL_EXISTENCE:
            return handleResetEmailExistence(state);
        default:
            return state;
    }
}

function handleVerifyUserRequest(state: IUserState): IUserState {
    return {
        ...state,
        isLoading: true,
    };
}

function handleVerifyUserFailure(state: IUserState): IUserState {
    return {
        ...state,
        isLoading: false,
    };
}

function handleAuthenticationSuccess(state: IUserState, action: UserActions.IAuthenticationSuccess): IUserState {
    const projectExistence: boolean = !!(action.payload.projects && action.payload.projects.length);

    return {
        ...state,
        isAuthenticationSuccessful: true,
        isLoading: false,
        user: action.payload,
        selectedProject: projectExistence ? action.payload.projects[0].projectId : '',
        selectedTeam:
            projectExistence && action.payload.teams && action.payload.teams.length
                ? action.payload.teams.find((x) => x.projectId === action.payload.projects[0].projectId).teamId
                : '',
    };
}

function handleRegistrationSuccess(state: IUserState): IUserState {
    return {
        ...state,
        wasCustomerCreated: true,
    };
}

function handleAuthenticationFailure(state: IUserState): IUserState {
    return {
        ...state,
        isLoading: false,
        isAuthenticationSuccessful: false,
    };
}

function handleGetUser(
    state: IUserState,
    action: UserActions.IAddUser | UserActions.IUpdateProfileSettingsSuccess | UserActions.IVerifyUserSuccess
): IUserState {
    const projectExistence: boolean = !!(action.payload.projects && action.payload.projects.length);

    return {
        ...state,
        user: {
            ...state.user,
            ...action.payload,
        },
        selectedProject: projectExistence ? action.payload.projects[0].projectId : '',
        selectedTeam:
            projectExistence && action.payload.teams && action.payload.teams.length
                ? action.payload.teams.find((x) => x.projectId === action.payload.projects[0].projectId).teamId
                : '',
        isLoading: false,
    };
}

function handleHideCustomerSuccessfulRegistration(state: IUserState): IUserState {
    return {
        ...state,
        wasCustomerCreated: false,
    };
}

function handleUpdateAvatarLink(state: IUserState, action: UserActions.IUpdateAvatarSuccess): IUserState {
    return {
        ...state,
        user: {
            ...state.user,
            avatarLink: action.payload,
        },
    };
}

function handleChangeUserProject(state: IUserState, action: UserActions.IChangeUserProject): IUserState {
    return {
        ...state,
        selectedProject: action.payload,
    };
}

function handleChangeUserTeam(state: IUserState, action: UserActions.IChangeUserTeam): IUserState {
    return {
        ...state,
        selectedTeam: action.payload,
    };
}

function handleUserLogOut(): IUserState {
    return initialState;
}

function handleEmailExistence(state: IUserState, action: UserActions.ICheckEmailExistenceSuccess): IUserState {
    return {
        ...state,
        emailExists: action.payload,
    };
}

function handleResetEmailExistence(state: IUserState): IUserState {
    return {
        ...state,
        emailExists: false,
    };
}
