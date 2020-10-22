import { IStory, IStoryColumns } from "../../types/storyTypes";
import { IUser } from "../../types/userTypes";

export interface IState {
  currentUser: ICurrentUserState;
  stories: IStoryState;
}

export interface ICurrentUserState {
  access_token: string;
  team: IUser[];
  user: IUser | null;
}

export interface IStoryState {
  columns: IStoryColumns[];
  selectedStory: IStory | null | undefined;
}
