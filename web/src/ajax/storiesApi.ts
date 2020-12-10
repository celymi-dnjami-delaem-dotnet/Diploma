import { ColumnIds } from '../constants/boardConstants';
import * as routeConstants from '../constants/routeConstants';
import { IJsonPatchBody } from '../types';
import { IStory, IStoryUpdate } from '../types/storyTypes';
import * as axios from './index';

export async function changeStoryColumn(jsonPatchDocument: IJsonPatchBody[]) {
    const response = await axios.axiosPatch(routeConstants.StoriesUrls.boardMove, jsonPatchDocument);

    return response.data;
}

export async function createStory(story: IStory) {
    const mappedStory = {
        columnType: ColumnIds.ToDo,
        description: story.description,
        estimate: story.estimate,
        isBlocked: false,
        isDefect: false,
        isReady: false,
        notes: story.notes,
        priority: story.storyPriority,
        recordVersion: 0,
        sprintId: story.sprintId,
        title: story.title,
        userId: story.userId,
        creationDate: new Date(),
    };

    const response = await axios.axiosPost(routeConstants.StoriesUrl, mappedStory);

    return response.data;
}

export async function updateStory(storyUpdate: IStoryUpdate) {
    const response = await axios.axiosPut(routeConstants.StoriesUrls.partUpdate, storyUpdate);

    return response.data;
}
