//UI routes
export const DefaultRoute = '/';
export const TestDefaultRoute = '/testmain';
export const FullViewStoryRoute = '/full-view/:storyId';
export const ProjectBoardRoute = '/board/:projectId';
export const LoginScreenRoute = '/start/login';
export const RegistrationScreenRoute = '/start/registration';
export const ViewStoryHistoryRoute = '/history/:storyId';
export const TeamsViewerRoute = '/teams';
export const ProjectsViewerRoute = '/projects';
export const TeamManagementRoute = '/team/:teamId';
export const ProjectManagementRoute = '/project/:projectId';
export const NoMatchRoute = '*';

//WebAPI routes
const BaseUrl = process.env.REACT_APP_BACK_URL;
export const UsersUrl = BaseUrl + 'user';
export const StoriesUrl = BaseUrl + 'story';
export const TokenUrl = BaseUrl + 'token';
export const customerUrl = BaseUrl + 'customer';
export const ProjectUrl = BaseUrl + 'project';
export const SprintUrl = BaseUrl + 'sprint';
export const EpicUrl = BaseUrl + 'epic';

export const SprintUrls = {
    getEpicSprints: SprintUrl + '/epic/',
};

export const EpicUrls = {
    getProjectEpics: EpicUrl + '/project/',
};

export const StoriesUrls = {
    boardMove: StoriesUrl + '/board-move',
    partUpdate: StoriesUrl + '/part-update',
};

export const CustomerUrls = {
    customerProjects: customerUrl + '/projects',
};

export const ProjectUrls = {
    getProject: ProjectUrl,
};

export const ProjectUrlUser = ProjectUrl + '/user';
